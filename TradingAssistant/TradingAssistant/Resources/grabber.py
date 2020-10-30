import sqlite3

class Grabber:
    def __init__(self, name: str = '', home: str = '', conn: sqlite3.Connection = None):
        self._name = name
        self._home = home
        self._conn = conn

    @property
    def name(self) -> str:
        return self._name

    @property
    def home(self) -> str:
        return self._home

    @staticmethod
    def get_exchange_id_by_name(conn: sqlite3.Connection, name: str) -> int:
        if not isinstance(conn, sqlite3.Connection):
            return 0
        result: tuple = None
        cur: sqlite3.Cursor = conn.cursor()
        try:
            cur.execute(f'SELECT ID FROM San WHERE MaSan="{name}"')
            result = cur.fetchone()
        except Exception as ex:
            print(f'{__file__}[26][Exeception]: {ex}')
            return 0
        if isinstance(result, tuple):
            if len(result) > 0:
                return int(result[0])
        return 0

    @staticmethod
    def get_stock_id_by_name(conn: sqlite3.Connection, name: str) -> int:
        if not isinstance(conn, sqlite3.Connection):
            return 0
        result: tuple = None
        cur: sqlite3.Cursor = conn.cursor()
        try:
            cur.execute(f'SELECT ID FROM CoPhieu WHERE MaCoPhieu="{name}"')
            result = cur.fetchone()
        except Exception as ex:
            print(f'{__file__}[47][Exception]: {ex}')
            return 0
        
        if isinstance(result, tuple):
            if len(result) > 0:
                return int(result[0])
        return 0
        
    
    def grab(self, start: int = 1, count: int = 10000) -> int:
        pass