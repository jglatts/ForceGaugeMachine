"""
    Class for serial communication between MxMoonFree ForceGauge and Windows machines 

    BAUD 9600 FOR MXMOONFREE

    Copyright: Z-Axis Connector Company
    Date: 2/22/23
    Author: John Glatts
"""
import serial
import datetime
import time
from time import sleep

class ForceReader():

    def __init__(self, write_out=False, port=4, baud=115200):
        self.port = port
        self.bauds = [9600, 115200]
        self.board = None
        self.out_file = open("testData.txt", "a")
        self.write_out = write_out

    def main(self):
        if (self.get_board_with_baud(100)):
            self.run()
        else:
            print('\nNo Board Connected!\nCheck USB\n')

    def get_board_with_baud(self, tries):
        index = 0
        count = 0
        while (count < tries):
            self.baud = self.bauds[index]
            print("baud = " + str(self.baud))
            if not self.get_board():
                print('\nNo Board Connected!\n')
                index += 1
                if (index > 1):
                    index = 0
                count += 1
            else:
                sleep(1)
                self.board.write(b'start')
                self.board.flush()
                print('sent data')
                return True
        
        return False

    def get_board(self):
        com_port = 'COM' + str(self.port)
        print("trying " + com_port)
        try: 
            self.board = serial.Serial(port=com_port, baudrate=self.baud, timeout=.2)
            #self.board = serial.Serial(port=com_port, baudrate=self.baud, timeout=.2, dsrdtr=None)
            #self.board.setRTS(False)
            #self.board.setDTR(False)
            return True
        except:
            return False

    def run(self):
        print('GETTING DATA\n')
        dateInfo = datetime.datetime.now()
        if (self.write_out):
            data = str(dateInfo.month) + "-" + str(dateInfo.day) + "-" + str(dateInfo.year) + "-" + str(dateInfo.hour) + "-" + str(dateInfo.minute)
            self.write_to_file("\n\n" + "Test Data: " + data + "\n\n");

        while (1):
            uart_str = self.get_uart_str()
            print(uart_str)
            if (self.write_out):
                self.write_to_file(uart_str)
    
    def get_uart_str(self):
        print(self.board.read())
        ret = self.board.read_until().decode()
        return ret

    def write_to_file(self, data_str):
        self.out_file.write(data_str)    


if __name__ == '__main__':
    ForceReader().main()
