using System;
using System.Net;
using System.Net.Sockets;

namespace TetrisMonoGame {

    class NetworkManager {


        /// <summary>
        /// Attempts to connect to the Tetris MonoGame server through TCP
        /// </summary>
        public static void Connect() {

            TcpClient tetrisClient = new TcpClient(Constants.ipAddress, Constants.port);


        }


        public static void Disconnect() {


        }


        public static void SendBlockInit() {


        }


        public static void SendBlockUpdate() {


        }


        public static void SendBlockDestroy() {


        }
    }
}
