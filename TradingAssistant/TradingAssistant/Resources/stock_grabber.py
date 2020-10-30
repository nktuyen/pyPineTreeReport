import sys
import os
import json
import requests
import sqlite3
import optparse
from grabber import Grabber
import hose_grabber
import hnx_grabber
import upcom_grabber

if __name__ == "__main__":
    opt: optparse.OptionParser = optparse.OptionParser('%prog exchange')
    opt.add_option('-s', '--start', help='Start index', default=1)
    opt.add_option('-c', '--count', help='Number of items', default=None)
    options,arguments = opt.parse_args()

    if len(arguments) != 1:
        print('Invalid number of argument')
        sys.exit(1)
    
    exch: str = arguments[0]
    if exch.lower() not in ['upcom', 'hnx', 'hose', 'uc']:
        print('Invalid exchange name')
        sys.exit(2)

    start: int = 0
    count: int = 0
    if options.start is not None:
        try:
            start = int(options.start)
        except:
            start = None
        if not isinstance(start, int):
            print(f'Invalid start index value')
            sys.exit(3)
    else:
        start = int(1)
    
    if start <= 0:
        print(f'Start index must greater than 0')
        sys.exit(4)

    if options.count is not None:
        try:
            count = int(options.count)
        except:
            count = None
        if not isinstance(count, int):
            print(f'Invalid number of items')
            sys.exit(5)
    else:
        count = int(10000)
    
    if count <= 0:
        print(f'Number of items must greater than 0')
        sys.exit(6)
    
    conn: sqlite3.Connection = None
    try:
        conn = sqlite3.connect('Stock.db')
    except:
        sys.exit(3)
    
    grabber: Grabber = None
    if exch.lower() =='hose':
        grabber = hose_grabber.HoseGrabber(conn)
    elif exch.lower() == 'hnx':
        grabber = hnx_grabber.HnxGrabber(conn)
    elif exch.lower() == 'uc' or exch.lower() == 'upcom':
        grabber = upcom_grabber.UPCoMGrabber(conn)
    
    grabber.grab(start=start, count=count)
