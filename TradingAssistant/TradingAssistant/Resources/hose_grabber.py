import grabber
import requests
from AdvancedHTMLParser import AdvancedHTMLParser, MultipleRootNodeException, AdvancedTag, TagCollection
from urllib.parse import unquote
from html import unescape
from html2text import html2text
import sqlite3

class HoseGrabber(grabber.Grabber):
    def __init__(self, conn: sqlite3.Connection):
        super().__init__(name='HoSE', home='https://www.hsx.vn/', conn=conn)

    def grab(self, start: int = 1, count: int = 10000) -> int:
        print(f'start={start}, count={count}')
        index: int = start
        cnt: int = 0
        cur: sqlite3.Cursor = None
        result: tuple = None
        if isinstance(self._conn, sqlite3.Connection):
            cur = self._conn.cursor()
        exch: int = grabber.Grabber.get_exchange_id_by_name(self._conn, self._name)
        while (index-start<count):
            url: str = f'{self._home}Modules/Listed/Web/SymbolView?id={index}'
            print(f'url:{url}')
            res: requests.Response = None
            try:
                res = requests.get(url)
            except Exception as ex:
                print(f'{__file__}[14]: Exception:{ex}')
                index += 1
                continue

            if res.status_code != 200:
                index += 1
                continue

            if not isinstance(res.text, str):
                index += 1
                continue

            parser: AdvancedHTMLParser = AdvancedHTMLParser()
            try:
                parser.parseStr(res.text)
            except MultipleRootNodeException as ex:
                pass
            
            res.close()
            if not isinstance(parser.body, AdvancedTag):
                index += 1
                continue

            body: AdvancedTag = parser.body
            tags: TagCollection = None
            try:
                tags = body.getElementsByXPath('/div[2]/div[1]/div[1]/div[1]/h3')
            except:
                index += 1
                continue
            
            if tags is None or not isinstance(tags, TagCollection):
                index += 1
                continue

            if len(tags) <= 0:
                index += 1
                continue
            
            h3: AdvancedTag = tags[0]
            if not isinstance(h3, AdvancedTag):
                index += 1
                continue

            text: str = html2text(unescape(h3.innerText.strip())).strip()
            parts: list = text.split('-')
            stock: str = parts[0]
            company: str = text[len(stock)+1:]
            stock = stock.strip().upper()
            company = company.strip()
            company = company.strip()
            words: list = company.split(' ')
            modified_words: list = list()
            for w in words:
                if w.lower() == 'ctcp':
                    w = 'Công ty cổ phần'
                if not w.isupper():
                    w = w.title()
                modified_words.append(w)
            company = ' '.join(modified_words)

            print(f'\tStock:{stock}')
            print(f'\tCompany:{company}')

            if isinstance(cur, sqlite3.Cursor):
                stock_id: int = grabber.Grabber.get_stock_id_by_name(self._conn, stock)
                if stock_id == 0:
                    cur.execute(f'INSERT INTO CoPhieu(MaCoPhieu,TenDoanhNghiep,SanNiemYet,NgayNiemYet)VALUES("{stock}","{company}",{exch},"")')
                else:
                    cur.execute(f'UPDATE CoPhieu SET TenDoanhNghiep="{company}",SanNiemYet={exch} WHERE ID={stock_id}')
                self._conn.commit()

            index += 1
            cnt += 1

        return cnt
