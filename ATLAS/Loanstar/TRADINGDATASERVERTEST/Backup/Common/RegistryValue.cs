// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using Microsoft.Win32;

namespace Anetics.Common
{
  /// <summary>
  /// Reads and writes registry values for the Current User.
  /// </summary>
  public class RegistryValue
  {
    private static string name = "";
    
    private static RegistryKey softwareKey = null;
    private static RegistryKey developerKey = null;
    private static RegistryKey nameKey = null;
    private static RegistryKey sectionKey = null;

    /// <summary>
    /// Set or Get the applicatioin name.
    /// </summary>
    public static string Name
    {
      set
      {
        name = value;

        try
        {
          softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
          
          if (softwareKey == null)
          {
            softwareKey = Registry.CurrentUser.CreateSubKey("Software");
          }
          
          if (softwareKey == null)
          {
            Log.Write("Unable to open or create a CurrentUser sub-key for /Software. [RegistryValue.Name(set)]", Log.Error, 1);
            name = "";
            return;
          }
        
          developerKey = softwareKey.OpenSubKey(Standard.Developer, true);
          
          if (developerKey == null)
          {
            developerKey = softwareKey.CreateSubKey(Standard.Developer);
          }
          
          if (developerKey == null)
          {
            Log.Write("Unable to open or create a CurrentUser sub-key for /Software/" + Standard.Developer + ". [RegistryValue.Name(set)]", Log.Error, 1);
            name = "";
            return;
          }
        
          nameKey = developerKey.OpenSubKey(name, true);
          
          if (nameKey == null)
          {
            nameKey = developerKey.CreateSubKey(name);
          }
          
          if (nameKey == null)
          {
            Log.Write("Unable to open or create a CurrentUser sub-key for /Software/" + Standard.Developer + "/" + name + ". [RegistryValue.Name(set)]", Log.Error, 1);
            name = "";
            return;
          }
        }
        catch(Exception e)
        {
          Log.Write(e.Message + " [RegistryValue.Name(set)]", Log.Error, 1);
          name = "";
        }
        finally
        {      
          if (nameKey != null)
          {
            nameKey.Close();
            nameKey = null;
          }

          if (developerKey != null)
          {
            developerKey.Close();
            developerKey = null;
          }
          
          if (softwareKey != null)
          {
            softwareKey.Close();
            softwareKey = null;
          }
        }
      }

      get
      {
        return name;
      }
    }

    /// <summary>
    /// Sets a registry value by section and key name.
    /// </summary>
    public static void Write(string section, string keyName, string keyValue)
    {
      if (name.Equals(""))
      {
        Log.Write("Attempt to write a static value with Name not set. [RegistryValue.Write]", Log.Information, 1);
        return;
      }

      try
      {
        softwareKey = Registry.CurrentUser.OpenSubKey("Software");
        developerKey = softwareKey.OpenSubKey(Standard.Developer);        
        nameKey = developerKey.OpenSubKey(name, true);        
        sectionKey = nameKey.OpenSubKey(section, true);

        if (sectionKey == null)
        {
          sectionKey = nameKey.CreateSubKey(section);
        }

        if (sectionKey == null)
        {
          Log.Write("Unable to open or create a CurrentUser sub-key for /Software/" + Standard.Developer + "/" + name + "/" + section + ".", Log.Error, 1);
          return;
        }
        
        sectionKey.SetValue(keyName, keyValue);
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [RegistryValue.Write]", Log.Error, 1);
      }
      finally
      {
        if (sectionKey != null)
        {
          sectionKey.Close();
          sectionKey = null;
        }

        if (nameKey != null)
        {
          nameKey.Close();
          nameKey = null;
        }

        if (developerKey != null)
        {
          developerKey.Close();
          developerKey = null;
        }
          
        if (softwareKey != null)
        {
          softwareKey.Close();
          softwareKey = null;
        }
      }
    }

    /// <summary>
    /// Gets a registry value by section and key name.
    /// </summary>
    public static string Read(string section, string keyName)
    {
      return Read (section, keyName, "");
    }

    /// <summary>
    /// Gets a registry value by section and key name or sets and returns a default if not found.
    /// </summary>
    public static string Read(string section, string keyName, string defaultKeyValue)
    {
      string keyValue = null;

      if (name.Equals(""))
      {
        Log.Write("Attempt to read a static value with Name not set. [RegistryValue.Read]", Log.Information, 1);
        return defaultKeyValue;
      }
      
      try
      {
        softwareKey = Registry.CurrentUser.OpenSubKey("Software");
        developerKey = softwareKey.OpenSubKey(Standard.Developer);        
        nameKey = developerKey.OpenSubKey(name, true);        
        sectionKey = nameKey.OpenSubKey(section, true);

        if (sectionKey == null)
        {
          sectionKey = nameKey.CreateSubKey(section);       
          
          if (sectionKey == null)
          {
            Log.Write("Unable to open or create a CurrentUser sub-key for /Software/" + Standard.Developer + "/" + name + "/" + section + ".", Log.Error, 1);            
            return defaultKeyValue;
          }
          else        
          {
            sectionKey.SetValue(keyName, defaultKeyValue);
            return defaultKeyValue;
          }
        }

        keyValue = (string)sectionKey.GetValue(keyName);
        
        if (keyValue == null)
        {
          keyValue = defaultKeyValue;
          sectionKey.SetValue(keyName, defaultKeyValue);
        }

        return keyValue;
      }
      catch(Exception e)
      {
        Log.Write(e.Message + " [RegistryValue.Read]", Log.Error, 1);
        return keyValue;
      }
      finally
      {
        if (sectionKey != null)
        {
          sectionKey.Close();
          sectionKey = null;
        }

        if (nameKey != null)
        {
          nameKey.Close();
          nameKey = null;
        }

        if (developerKey != null)
        {
          developerKey.Close();
          developerKey = null;
        }
          
        if (softwareKey != null)
        {
          softwareKey.Close();
          softwareKey = null;
        }
      }
    }
  }  
}
