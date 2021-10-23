using System;
using System.Net.Sockets;

namespace TetrisMonoGameServer {
    class DataStream {

        // The raw byte data stream and the string that the stream gets parsed into
        public static byte[] dataStream;
        public static string dataInStream;

        // This is the 'actual' data stream
        public static NetworkStream networkStream;
    }
}