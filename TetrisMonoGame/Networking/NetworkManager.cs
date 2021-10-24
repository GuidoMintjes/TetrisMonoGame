using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TetrisMonoGame {

    class NetworkManager {

        static TcpClient tetrisClient;

        /// <summary>
        /// Attempts to connect to the Tetris MonoGame server through TCP
        /// </summary>
        public static void Connect() {

            // Set up the client instance to be able to connect to the server
            tetrisClient = new TcpClient(Constants.ipAddress, Constants.port);
            
            
            // Set up datastream for sending and receiving data
            DataStream.networkStream = tetrisClient.GetStream();
            DataStream.dataStream = new byte[256];


            DataStream.WriteString("Utrecht University is best university lesgo");  // Write a message to the stream

            DataStream.networkStream.Write(DataStream.dataStream);
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
