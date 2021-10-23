﻿namespace TetrisMonoGame {

    class NetworkManager {


        public static void Connect() {
            TCPChatClient.StartConnect();
        }


        public static void SendBlockInfo() {

            TCPClientSend.SendBlockInfo(GameWorld.blok);
        }

        internal static void Disconnect() {

            TCPChatClient.Disconnect();
        }
    }
}