
using System.IO;
using System.Runtime.InteropServices;
using FarNet;
namespace Shortie
{	
	[ModuleFiler(Name = "Shortie Filer", Mask = "*.lnk")]
    [Guid("F7988E2A-1050-425C-BDD8-C6583D23B2D1")]
	public class ShortieFiler : ModuleFiler
	{		
		public override void Invoke(object sender, ModuleFilerEventArgs e)
		{
			try
			{
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                var link = shell.CreateShortcut(e.Name);
                var targetFileInfo = new FileInfo(link.TargetPath as string);
                ShortieCommand.ProcessShortcut(targetFileInfo);
			}
			catch
			{
                //TODO Senthil 
			}
		}
	}
}
