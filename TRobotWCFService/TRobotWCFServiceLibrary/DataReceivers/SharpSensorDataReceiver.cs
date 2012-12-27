﻿using TRobotWCFServiceLibrary.Messages;
using TRobotWCFServiceLibrary.TRobotDrivers;
using System;
using TRobotWCFServiceLibrary.Utils;

namespace TRobotWCFServiceLibrary.DataReceivers
{
    internal class SharpSensorDataReceiver : IDataReceiver
    {
        private const int arduinoBaudRate = 9600;
        private const string arduinoComPort = "COM11";
        private Arduino arduino;
        private const string key = "distance";

        public SharpSensorDataReceiver()
        {
            arduino = new Arduino(arduinoComPort, arduinoBaudRate);
        }

        /// <summary>
        ///     This method returns distance for each sharp sensor in mm.
        ///     Range for this sensor is (200 - 1500 mm).
        ///     The keys for measured distance are 'distance1', 'distance2', 'distance3' for three sharps.
        /// </summary>
        public Data ReceiveData()
        {
            Data data = new Data();
            data.DataReceiverType = DataReceiver.Sharp;
            try
            {
                arduino.Connect();
                String[] distances = arduino.GetSharpsData();
                
                for (int i = 1; i <= distances.Length; i++)
                {
                    Int16 hexToDecimal = Convert.ToInt16(distances[i - 1], 16);
                    double distance = (60.495 * Math.Pow(hexToDecimal * 0.0049, -1.1904) * 10);

                    if (distance >= 200 && distance <= 1500)
                    {
                        data.Dictionary.Add(key + i, distance);
                    }
                    else
                    {
                        data.Dictionary.Add(key + i, 0);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
            finally
            {
                arduino.Disconnect();
            }
            return data;
        }
    }
}