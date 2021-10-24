using System;
using System.Net.Sockets;

namespace TetrisMonoGame {
    class TetrisClient : TcpClient {

        DataStream mainStream;

        public void ConnectCallback(IAsyncResult ar) {

            mainStream = new DataStream();

            EndConnect(ar);

            DataStream.networkStream = GetStream();
            //DataStream.networkStream.BeginRead(DataStream.dataStream, 0, Constants.bufferSize, StreamReadCallback, null);
        }


        private void StreamReadCallback(IAsyncResult ar) {

            try {

                int streamLength = DataStream.networkStream.EndRead(ar);

                byte[] receivedData = new byte[streamLength];

                CheckDataType(receivedData);

            } catch {

                Close();
            }
        }


        void CheckDataType(byte[] receivedData) {

            //int dataType = DataStream.ReadInt
        }
    }
}