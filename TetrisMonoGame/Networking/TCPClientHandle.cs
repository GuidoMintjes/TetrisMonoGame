﻿using System;

namespace TetrisMonoGame {
    class TCPClientHandle {


        // Confirm that the welcome message has been received (and display)
        public static void WelcomeReturn(Packet packet) {


            string welcomeMSG = packet.PacketReadString(true);

            int thisClientID = packet.PacketReadInt(false);

            Console.Clear();

            Funcs.printMessage(3, welcomeMSG, false);
            TCPChatClient.clientID = thisClientID;
            TCPClientSend.ReceivedWelcome();
        }


        public static void DisplayMessage(Packet packet) {

            string message = packet.PacketReadString(true);

            Funcs.printMessage(3, message, false);
        }


        public static void DisplayChat(Packet packet) {

            string message = packet.PacketReadString(true);

            Funcs.printMessage(4, message, false);
        }


        public static void DisplayConnected(Packet packet) {

            string userName = packet.PacketReadString(true);

            Funcs.printMessage(3, $"{userName} has connected to this chat room!", false);
        }



        public static void DisplayDisconnected(Packet packet) {

            string userName = packet.PacketReadString(true);

            Funcs.printMessage(3, $"{userName} has disconnected from this chat room!", false);
        }


        public static void DisplayNames(Packet packet) {

            packet.PacketReadInt(false);

            int names;

            try {
                names = packet.PacketReadInt(true);
            } catch {
                names = 0;
                packet.readPointer += 4;
            }

            if (names != 0) {
                Console.WriteLine();
                Funcs.printMessage(3, $"These people are already online: ", false);

                /*
                foreach (byte byt in packet.GetPacketBytes()) {

                    Console.Write(byt);
                }
                Console.WriteLine();
                Console.WriteLine("NameList size: " + packet.GetPacketBytes().Length);
                */


                for (int i = 1; i <= names; i++) {

                    Funcs.printMessage(3, $"    {packet.PacketReadString(true)}", false);
                }

                Console.WriteLine();
            } else {

                Funcs.printMessage(3, "No one else is online yet!", false);
            }
        }


        public static void CatchZeroHandler(Packet packet) {

            Funcs.printMessage(0, "Packet handler with id 0 used!", false);
        }


        public static void UpdateOpponentBlock(Packet packet) {

            Funcs.printMessage(2, "Trying to update Opponent block!", false);

            int blockX = packet.PacketReadInt(true);
            int blockY = packet.PacketReadInt(true);

            int shapeSize = packet.PacketReadInt(true);
            bool[,] shapeReceived = new bool[shapeSize, shapeSize];


            for (int x = 0; x < shapeSize; x++) {
                for (int y = 0; y < shapeSize; y++) {

                    shapeReceived[x, y] = packet.PacketReadBool(true);
                }
            }

            int blockColour = packet.PacketReadInt(true);
        }
    }
}