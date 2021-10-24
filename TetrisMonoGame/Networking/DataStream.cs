using System;
using System.Net.Sockets;
using System.Text;

namespace TetrisMonoGame {
    class DataStream {

        // The raw byte data stream and the string that the stream gets parsed into
        public static byte[] dataStream;
        public static string dataInStream;

        public static int writePointer;
        public static int readPointer;


        // This is the 'actual' data stream
        public static NetworkStream networkStream;


        /// <summary>
        /// This method will write a string to the byte array data stream
        /// </summary>
        public static void WriteString(string msg) {
            
            byte[] message = Encoding.ASCII.GetBytes(msg);  // Convert the message string to bytes

            foreach (byte byt in message) {

                dataStream.SetValue(byt, writePointer);
                writePointer ++;
            }
        }
    }
}