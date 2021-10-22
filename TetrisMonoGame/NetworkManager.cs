using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisMonoGame {

    class NetworkManager {


        public static void Connect() {
            TCPChatClient.StartConnect();
        }


        public static void SendBlockInfo() {

            TCPClientSend.SendBlockInfo(GameWorld.blok);
        }
    }
}