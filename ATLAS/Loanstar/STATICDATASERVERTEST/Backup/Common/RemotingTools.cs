// Licensed Materials - Property of Anetics, LLC.
// (C) Copyright Anetics, LLC. 2003, 2004  All rights reserved.

using System;
using System.Collections;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;

namespace Anetics.Common
{
  /// <summary>
  /// Useful tools for the implementation of Remoting.
  /// </summary>
  public class RemotingTools
  {
    /// <summary>
    /// Returns the object associated with a specific Remoting type.
    /// </summary>
    public static Object ObjectGet(Type type)
    {
      WellKnownClientTypeEntry[] wellKnownClients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
      
      if (wellKnownClients.Length.Equals(0))
      {
        RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        wellKnownClients = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
      }


      foreach(WellKnownClientTypeEntry wellKnownClient in wellKnownClients) 
      {
        if(wellKnownClient.ObjectType == type)
        {
          return Activator.GetObject(wellKnownClient.ObjectType, wellKnownClient.ObjectUrl);
        }
      }

      return null;
    }
    
    /// <summary>
    /// Returns a new TCP channel set up on the port specified using a binary formatter.
    /// </summary>
    public static TcpChannel TcpChannelGet(int port)
    {
      IDictionary channelProperties = new Hashtable();
      channelProperties["port"] = port;

      BinaryClientFormatterSinkProvider binaryClientFormatterSinkProvider = new BinaryClientFormatterSinkProvider(); 

      BinaryServerFormatterSinkProvider binaryServerFormatterSinkProvider = new BinaryServerFormatterSinkProvider(); 
      binaryServerFormatterSinkProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full; 
      
      return new TcpChannel(channelProperties, binaryClientFormatterSinkProvider, binaryServerFormatterSinkProvider);
    }
    
    /// <summary>
    /// Returns a new HTTP channel set up on the port specified using a binary formatter.
    /// </summary>
    public static HttpChannel HttpChannelGet(int port)
    {
      IDictionary channelProperties = new Hashtable();
      channelProperties["port"] = port;

      BinaryClientFormatterSinkProvider binaryClientFormatterSinkProvider = new BinaryClientFormatterSinkProvider(); 

      BinaryServerFormatterSinkProvider binaryServerFormatterSinkProvider = new BinaryServerFormatterSinkProvider(); 
      binaryServerFormatterSinkProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full; 
      
      return new HttpChannel(channelProperties, binaryClientFormatterSinkProvider, binaryServerFormatterSinkProvider);
    }
  }
}
