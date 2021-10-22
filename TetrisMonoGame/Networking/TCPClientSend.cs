using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TetrisMonoGame {
    class TCPClientSend {
    
        // Acknowledge the welcome message and send back client ID as confirmation and username
        public static void ReceivedWelcome() {

            Packet packet = new Packet((int) ClientPackets.welcomeReceived);

            packet.PacketWrite(TCPChatClient.clientID);
            packet.PacketWrite(TCPChatClient.userName);
            
            TCPSendData(packet);

            Funcs.printMessage(2, $"Sent welcome received packet with username {TCPChatClient.userName} and this is ID {TCPChatClient.clientID}",
                                false);
        }


        public static void SendBlockInfo(Block block) {

            Funcs.printMessage(2, "Trying to send block info!", false);

            using (Packet packet = new Packet(7)) {

                packet.PacketWrite((int) block.Pos.X);
                packet.PacketWrite((int) block.Pos.Y);

                int shapeSize = block.GetShape().GetLength(0);
                packet.PacketWrite(shapeSize);

                for (int x = 0; x < shapeSize; x++) {
                    for (int y = 0; y < shapeSize; y++) {

                        packet.PacketWrite(block.GetShape()[x, y]);
                    }
                }

                packet.PacketWrite(block.ColorInt);

                TCPSendData(packet);
            }
        }

        
        // Method to call to start to send an actual packet
        public static void TCPSendData(Packet packet) {

            packet.PacketWriteLength();
            TCPChatClient.tcp.TCPSendData(packet);
        }
    }
}