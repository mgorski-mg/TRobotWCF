﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRobotWCFServiceLibrary.Messages;
using TRobotWCFServiceLibrary.TRobotDrivers;

namespace TRobotWCFServiceLibrary.DataProvider
{
    class EncoderDataProvider:IDataProvider
    {
        private Roboteq roboteQ;
        private Data driversData;

        public EncoderDataProvider(Data driversData, Roboteq roboteQ) 
        {
            this.driversData = driversData;
            this.roboteQ = roboteQ;
        }

        public bool ProvideData()
        {
            try
            {
                roboteQ.DrivePulses((int)driversData.Dictionary["leftWheel"], (int)driversData.Dictionary["rightWheel"]);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
