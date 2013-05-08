using System;
using System.Configuration;

namespace WorldWideClient
{
  /// <summary>
  /// Useful common static functions.
  /// </summary>
  public class Tools
  {
    /// <summary>
    /// Returns the value '[Null]' if anyString is null or zero-length.
    /// </summary>
    public static string ZeroLengthNull(string anyString)
    {
      if ((anyString == null) || anyString.Length.Equals(0))
      {
        return "[Null]";
      }
      else
      {
        return anyString;
      }
    }

    /// <summary>
    /// Returns anyDate formatted per formatString or a zero-length string if anyDate is not a date.
    /// </summary>
    public static string FormatDate(string anyDate, string formatString)
    {
      return FormatDate(anyDate, formatString, 0);
    }

    /// <summary>
    /// Returns anyDate adjusted by offset minutes formatted per formatString or a zero-length string if anyDate is not a date.
    /// </summary>
    public static string FormatDate(string anyDate, string formatString, short offset)
    {
      try
      {
        return DateTime.Parse(anyDate).AddMinutes(offset).ToString(formatString);
      }
      catch
      {
        return "";
      }
    }

    /// <summary>
    /// Returns true if anyDate is a valid date.
    /// </summary>
    public static bool IsDate(string anyDate)
    {
      try
      {
        DateTime d = DateTime.Parse(anyDate);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Returns true if anyValue is numerical.
    /// </summary>
    public static bool IsNumeric(string anyValue)
    {
      try
      {
        decimal.Parse(anyValue);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Returns the item at index from itemSource where delimited by delimiter.
    /// </summary>
    public static string SplitItem(string itemSource, string delimiter, int index)
    {
      string [] item = null;

      if ((itemSource.Length.Equals(0)) || (delimiter.Length.Equals(0)) || (index < 0))
      {
        return "";
      }

      char [] delimiterChars = delimiter.ToCharArray();

      item = itemSource.Split(delimiterChars, index + 2);
      
      if (item.Length > index)
      {
        return item[index];
      }
      else
      {
        return "";
      }
    }

    /// <summary>
    /// Returns the long interger component of a numeric text value.
    /// </summary>
    public static long ParseLong(string anyNumeric)
    {
      try
      {
        return long.Parse(Tools.SplitItem(anyNumeric, ".", 0).Replace(",", ""));
      }
      catch
      {
        return 0;
      }
    }

    /// <summary>
    /// Returns a string of number formatted per format.
    /// </summary>
    public static string FormatPadNumber(string number, string format)
    {
     return FormatPadNumber(number, format, (short)format.Length);
    }           

    /// <summary>
    /// Returns a string of number formatted per format padded with blanks to length.
    /// </summary>
    public static string FormatPadNumber(string number, string format, short length)
    {
      decimal d;
      string result;

      try
      {        
        d = decimal.Parse(number);
        result = d.ToString(format).PadLeft(length, ' ');            
      }
      catch
      {
        return new String(' ', length);
      }
  
      if (result.Length > length)
      {
        return new String('*', length);
      }

      return result;
    }           
    
    /// <summary>
    /// Returns a string of text padded with blanks to length.
    /// </summary>        
    public static string FormatPadText(string text, short length)
    {
      return FormatPadText(text, ' ', length);
    }

    /// <summary>
    /// Returns a string of text padded with padCharacter to length.
    /// </summary>        
    public static string FormatPadText(string text, char padCharacter, short length)
    {
      string result;

      try
      {
        result = text.PadRight(length, padCharacter);
      }
      catch
      {
        return new String(' ', length);
      }
  
      return result.Substring(0, length);
    }
  }
}