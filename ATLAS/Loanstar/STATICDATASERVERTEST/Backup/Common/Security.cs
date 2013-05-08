// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Data;

namespace Anetics.Common
{
  public class Security
  {
    public static string SymbolDelimiterSet(string symbol)
    {
      symbol = symbol.Trim();

      symbol = symbol.Replace('/', '.');
      symbol = symbol.Replace('\\', '.');
      symbol = symbol.Replace('-', '.');
      symbol = symbol.Replace(' ', '.');
      symbol = symbol.Trim();

      return symbol;
    }
    
    public static bool IsCusip(string secId)
    {
      if (secId.Trim().Length != 9)
      {
        return false;
      }

      string okChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      
      for (byte j = 0; j < 8; j++)
      {
        if (okChars.IndexOf(secId.Substring(j, 1).ToUpper()) == -1)
        {
          return false;
        }
      }

      okChars = "0123456789";
      
      if (okChars.IndexOf(secId.Substring(8, 1)) == -1)
      {
        return false;
      }

      // ToDo: Check digit.
      
      return true;
    }

    public static bool IsSymbol(string secId)
    {
      secId = SymbolDelimiterSet(secId);
      
      int j = secId.Length;      
      
      if ((j < 1)||(j > 8))
      {
        return false;
      }

      string okChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.";
      
      for (byte i = 0; i < j; i++)
      {
        if (okChars.IndexOf(secId.Substring(i, 1).ToUpper()) == -1)
        {
          return false;
        }
      }

      return true;
    } 
 
    public static bool IsSedol(string secId)
    {
      if (secId.Trim().Length != 7)
      {
        return false;
      }

      string okChars = "BCDFGHJKLMNPQRSTVWXYZ9"; // Numeral 9 included here to allow dummy codes.

      if (okChars.IndexOf(secId.Substring(0, 1).ToUpper()) == -1)
      {
        return false;
      }
      
      okChars = "BCDFGHJKLMNPQRSTVWXYZ0123456789";

      for (byte j = 1; j < 6; j++)
      {
        if (okChars.IndexOf(secId.Substring(j, 1).ToUpper()) == -1)
        {
          return false;
        }
      }

      okChars = "0123456789";
      
      if (okChars.IndexOf(secId.Substring(6, 1).ToUpper()) == -1)
      {
        return false;
      }

      // ToDo: Check digit.
      
      return true;
    }  

    public static bool IsIsin(string secId)
    {
      if (secId.Trim().Length != 12)
      {
        return false;
      }

      string okChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      
      for (byte j = 0; j < 2; j++)
      {
        if (okChars.IndexOf(secId.Substring(j, 1).ToUpper()) == -1)
        {
          return false;
        }
      }

      okChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      
      for (byte j = 2; j < 11; j++)
      {
        if (okChars.IndexOf(secId.Substring(j, 1).ToUpper()) == -1)
        {
          return false;
        }
      }

      okChars = "0123456789";

      if (okChars.IndexOf(secId.Substring(11, 1)) == -1)
      {
        return false;
      }

      // ToDo: Check digit.
      
      return true;
    }

    public static bool IsQuick(string secId)
    {
      int j = secId.Length; 
      
      if ((j < 4) || (j > 5))
      {
        return false;
      }

      string okChars = "0123456789";
      
      for (byte i = 0; i < j ; i++)
      {
        if (okChars.IndexOf(secId.Substring(i, 1)) == -1)
        {
          return false;
        }
      }

      return true;
    }

    public static bool IsSecId(string secId)
    {
      if (IsCusip(secId))
      {
        return true;
      }

      if (IsSymbol(secId))
      {
        return true;
      }

      if (IsSedol(secId))
      {
        return true;
      }

      if (IsIsin(secId))
      {
        return true;
      }

      if (IsQuick(secId))
      {
        return true;
      }

      return false;
    }

    public static bool IsCusipBond(string cusip)
    {
      if (cusip.Length < 9)
      {
        return false;
      }

      return ((cusip.Substring(6, 1).CompareTo("A") >= 0) || (cusip.Substring(7, 1).CompareTo("A") >= 0));
    }
  }
}
