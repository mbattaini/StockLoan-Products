//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The ModuleMetadataReflectionHelper class provides a helper to retrieve module relevant information
// like dependencies and the name of the module from a given assembly
// 
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/03-01-010-How_to_Create_Smart_Client_Solutions.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System.Reflection;
using Microsoft.Practices.CompositeUI;
using System.Collections.Generic;
using System.IO;

namespace StockLoan.Locates.Infrastructure.Library.Services
{
    public static class ModuleMetadataReflectionHelper
    {
        public static string GetModuleName(string assemblyFilename)
        {
            try
            {
                string assemblyFullPath = Path.GetFullPath(assemblyFilename);
                return GetModuleName(Assembly.LoadFile(assemblyFullPath));
            }
            catch
            {
                return null;
            }
        }

        public static string GetModuleName(Assembly assm)
        {
            foreach (ModuleAttribute attrib in assm.GetCustomAttributes(typeof(ModuleAttribute), false))
                return attrib.Name;

            return null;
        }

        public static IList<string> GetModuleDependencies(string assemblyFilename)
        {
            try
            {
                return GetModuleDependencies(Assembly.LoadFile(assemblyFilename));
            }
            catch
            {
                return new List<string>();
            }
        }

        public static IList<string> GetModuleDependencies(Assembly assm)
        {
            List<string> results = new List<string>();

            foreach (ModuleDependencyAttribute attrib in assm.GetCustomAttributes(typeof(ModuleDependencyAttribute), false))
                results.Add(attrib.Name);

            return results;
        }
    }
}
