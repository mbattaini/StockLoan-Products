using System;
using System.IO;
using System.Collections;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Anetics.Common;

namespace Anetics.S3
{	
	public class Zip
	{
		public void CreateZip(string[] filenames, string zipfilepath, int compression)
		{
			try
			{
				for( int i = 0; i < filenames.Length; i++ )
				{
					if (File.Exists(zipfilepath + @"\" + filenames[i]))
					{
						int lastIndexExt = filenames[i].LastIndexOf(".");
						string zipFileName = zipfilepath + filenames[i].Substring(0, lastIndexExt) + ".zip";
						Log.Write("zipFileName : " + zipFileName + ". [S3.CreateZip]", 3);

						using( FileStream tempFileStream = new FileStream(zipFileName , FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None ) )
						using( ZipOutputStream zipOutput = new ZipOutputStream( tempFileStream ) )
						{
							zipOutput.SetLevel( compression );
							string localFileName = filenames[i].ToString();
							Log.Write("Adding " + localFileName + " to " + zipFileName + ". [S3.CreateZip]", 3);

							using( FileStream fileStream = new FileStream( zipfilepath + localFileName, FileMode.Open, FileAccess.Read, FileShare.Read ) )
							{
								byte[] buffer = new byte[fileStream.Length];
								fileStream.Read( buffer, 0, buffer.Length );

								ZipEntry entry = new ZipEntry(localFileName);
								DateTime dt = File.GetLastWriteTime(zipfilepath + localFileName);
								entry.DateTime = dt;
								
								entry.Size = fileStream.Length;
								fileStream.Close();

								Log.Write("File date/time : " + entry.DateTime + ". [S3.CreateZip]", 3);
								Log.Write("File size : " + entry.Size + ". [S3.CreateZip]", 3);

								zipOutput.PutNextEntry( entry );
								zipOutput.Write( buffer, 0, buffer.Length );

								Log.Write("Successfully created: " + zipFileName, 3);
								buffer = null;
							}
						
							zipOutput.Finish();
							zipOutput.Flush();
							zipOutput.Close();											
						}
					}
					else
					{
						Log.Write("File " + filenames[i] + " not found. [S3.CreateZip]", 3);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("CreateZip Exception : " + ex.Message + " - [S3.CreateZip]");
			}
		}
		public void UnZipFile(string zipfile, string directoryname)
		{
			try
			{
				if (!File.Exists(zipfile))
				{
					Log.Write(zipfile + " not found. [S3.UnZipFile]", 3);
					throw new FileNotFoundException( zipfile + " not found");
				}

				using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipfile))) 
				{
					ZipEntry theEntry;
					while ((theEntry = s.GetNextEntry()) != null) 
					{
						string fileName = Path.GetFileName(theEntry.Name);
					
						if ( directoryname.Length > 0 ) 
							Directory.CreateDirectory(directoryname);
					
						if (fileName != String.Empty) 
						{
							using (FileStream streamWriter = File.Create(directoryname + "\\" + theEntry.Name)) 
							{
								int size = 2048;
								byte[] data = new byte[2048];
								Log.Write("Extracting " + theEntry.Name + " from " + zipfile + ". [S3.UnZipFile]", 3);
								while (true) 
								{
									size = s.Read(data, 0, data.Length);
									if (size > 0) 
									{
										streamWriter.Write(data, 0, size);
									} 
									else 
									{
										break;
									}
								}
							}
						}
					}
					Log.Write("Suscessfully extracted files from " + zipfile + ". [S3.UnZipFile]", 3);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("UnZipFile Exception : " + ex + " - [S3.UnZipFile]");	
			}
		}
		public string[] ReadZipFile(string zipfile)
		{
			ArrayList fileList = new ArrayList();
			try
			{
				if (!File.Exists(zipfile))
				{
					Log.Write(zipfile + " not found. [S3.ReadZipFile]", 3);
					throw new FileNotFoundException( zipfile + " not found");
				}

				byte[] data = new byte[4096];
				using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipfile)))
				{
					ZipEntry theEntry = s.GetNextEntry();
					while ((theEntry = s.GetNextEntry()) != null) 
					{
						fileList.Add(theEntry.Name);
						Log.Write("File : " + theEntry.Name + ". [S3.ReadZipFile]", 3);
					}
					s.Close();
				}
			}
			catch (Exception ex)
			{
				throw new Exception ("ReadZipFile Exception : " + ex + " - [S3.ReadZipFile]");	
			}

			return (string[]) fileList.ToArray(typeof(string));
		}

	}
}
