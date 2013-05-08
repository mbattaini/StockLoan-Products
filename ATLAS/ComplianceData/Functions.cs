﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsApplicationMQ
{
    public class Functions
    {
        public static string Status(string pendMadeFlag)
        {
            string flag = "";

            switch (pendMadeFlag)
            {
                case (""):
                case ("X"):
                    flag = "Made";
                    break;

                case ("P"):
                    flag = "Pending";
                    break;

                case ("C"):
                case ("D"):
                case ("K"):
                    flag = "Dropped";
                    break;

                default:
                    flag = pendMadeFlag;
                    break;
            }

            return flag;
        }

    }
}