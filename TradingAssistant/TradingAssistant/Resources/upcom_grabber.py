import grabber
import requests
from AdvancedHTMLParser import AdvancedHTMLParser, MultipleRootNodeException, AdvancedTag, TagCollection
from urllib.parse import unquote
from html import unescape
from html2text import html2text
import sqlite3

class UPCoMGrabber(grabber.Grabber):
    def __init__(self, conn: sqlite3.Connection):
        super().__init__(name='HNX', home='https://www.hnx.vn/', conn=conn)

    def _explore_stock(self, stock_code: str) -> dict:
        if not isinstance(stock_code, str):
            return dict()
        
        url: str = f'{self._home}cophieu-etfs/chi-tiet-chung-khoan-uc-{stock_code}.html'
        print(f'{__file__}[18]: url={url}')
        res: requests.Response = None
        try:
            res = requests.get(url)
        except Exception as ex:
            print(f'{__file__}[14]: Exception:{ex}')
            return dict()

        if res.status_code != 200:
            return dict()

        if not isinstance(res.text, str):
            return dict()

        parser: AdvancedHTMLParser = AdvancedHTMLParser()
        try:
            parser.parseStr(res.text)
        except MultipleRootNodeException as ex:
            pass
        
        res.close()
        if not isinstance(parser.body, AdvancedTag):
            return dict()

        body: AdvancedTag = parser.body
        tags: TagCollection = None
        try:
            tags = body.getElementsByXPath('/div[1]/div[2]/div[3]/div[5]/div[1]')
        except:
            return dict()
        
        if tags is None or not isinstance(tags, TagCollection):
            return dict()

        if len(tags) <= 0:
            return dict()
        
        table: AdvancedTag = tags[0]
        if not isinstance(table, AdvancedTag):
            return dict()
        
        title: AdvancedTag = None
        content: AdvancedTag = None
        key: str = ''
        val: str = ''
        date: str = ''
        klny: int = 0
        kllh: int = 0
        state: int = 0
        for div in table.childNodes:
            key = ''
            val = ''
            title =  div.firstElementChild
            content = None
            if isinstance(title, AdvancedTag):
                content = title.nextSiblingElement
                if title.hasChildNodes():
                    title = title.firstElementChild
                key = html2text(unescape(title.innerText.strip())).strip()
            if isinstance(content, AdvancedTag):
                if content.hasChildNodes():
                    content = content.firstElementChild
                val = html2text(unescape(content.innerText.strip())).strip()
            
            print(f'Key={key}, Value={val}')
            if key.lower() == 'Ngày GD đầu tiên'.lower():
                date = val
            elif key.lower() == 'KLNY (Cổ phiếu)'.lower() or key.lower() == 'KLĐKGD (Cổ phiếu)'.lower():
                klny = int(val.replace('.', ''))
            elif key.lower() == 'KLLH (Cổ phiếu)'.lower():
                kllh = int(val.replace('.', ''))
            elif key.lower() == 'Trạng thái giao dịch'.lower():
                state = val

        return {'date':date, 'klny':klny, 'kllh':kllh,'state':state}

    def grab(self, start: int = 1, count: int = 10000) -> int:
        print(f'start={start}, count={count}')
        index: int = start
        cnt: int = 0
        cur: sqlite3.Cursor = None
        result: tuple = None
        if isinstance(self._conn, sqlite3.Connection):
            cur = self._conn.cursor()
        exch: int = grabber.Grabber.get_exchange_id_by_name(self._conn, self._name)

        url: str = f'{self._home}vi-vn/cophieu-etfs/chung-khoan-uc.html'
        print(f'url:{url}')
        res: requests.Response = None
        try:
            res = requests.get(url)
        except Exception as ex:
            print(f'{__file__}[14]: Exception:{ex}')
            return 0

        if res.status_code != 200:
            return 0

        if not isinstance(res.text, str):
            return 0

        parser: AdvancedHTMLParser = AdvancedHTMLParser()
        try:
            parser.parseStr(res.text)
        except MultipleRootNodeException as ex:
            pass
        
        res.close()
        if not isinstance(parser.body, AdvancedTag):
            return 0

        body: AdvancedTag = parser.body
        tags: TagCollection = None
        try:
            tags = body.getElementsByXPath('/div[1]/div[2]/div[3]/div[2]/ul[1]/li[2]/select')
        except:
            return 0
        
        if tags is None or not isinstance(tags, TagCollection):
            return 0

        if len(tags) <= 0:
            return 0
        
        select: AdvancedTag = tags[0]
        if not isinstance(select, AdvancedTag):
            return 0
        
        opt: AdvancedTag = None
        for opt in select.childNodes:
            text: str = html2text(unescape(opt.innerText.strip())).strip()
            parts: list = text.split('-')
            stock: str = parts[0]
            company: str = text[len(stock)+1:]
            stock = stock.strip().upper()
            company = company.strip()
            words: list = company.split(' ')
            modified_words: list = list()
            for w in words:
                if w.lower() == 'ctcp':
                    w = 'Công Ty Cổ Phần'
                elif w.lower() == 'tmcp':
                    w = 'Thương Mại Cổ Phần'
                if not w.isupper():
                    w = w.title()
                modified_words.append(w)
            company = ' '.join(modified_words)
            stock_info = self._explore_stock(stock_code=stock)
            date: str = stock_info.get('date', '')
            klny: int = stock_info.get('klny', 0)
            kllh: int = stock_info.get('kllh', 0)
            state: str = stock_info.get('state', '')

            print(f'\tStock:{stock}')
            print(f'\tCompany:{company}')
            print(f'\tDate:{date}')
            print(f'\tKLNY:{klny}')
            print(f'\tKLLH:{kllh}')
            print(f'\tTTGD:{state}\n')

            if isinstance(cur, sqlite3.Cursor):
                stock_id: int = grabber.Grabber.get_stock_id_by_name(self._conn, stock)
                if stock_id == 0:
                    cur.execute(f'INSERT INTO CoPhieu(MaCoPhieu,TenDoanhNghiep,SanNiemYet,KhoiLuongNiemYet,KhoiLuongLuuHanh,NgayNiemYet)VALUES("{stock}","{company}",{exch},{klny},{kllh},"{date}")')
                else:
                    cur.execute(f'UPDATE CoPhieu SET TenDoanhNghiep="{company}",SanNiemYet={exch},KhoiLuongNiemYet={klny},KhoiLuongLuuHanh={kllh},NgayNiemYet="{date}" WHERE ID={stock_id}')
                self._conn.commit()

            index += 1
            cnt += 1

        return cnt