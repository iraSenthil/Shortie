
using FarNet;
using System.Collections.Generic;
using System.IO;
using System;
namespace Shortie
{
	/// <summary>
	/// The module host class.
	/// It is created and connected on the first call of any module job.
	/// </summary>
	[ModuleHost(Load = false)]
	public class ShortieHost : ModuleHost
	{
		public ShortieHost()
		{
			
		}
		/// <summary>
		/// This method is called once first of all.
		/// </summary>
		public override void Connect()
		{
		}
		/// <summary>
		/// This method is called once on exit.
		/// NOTE: it should not call the core.
		/// </summary>
		public override void Disconnect()
		{
		}
	}
}
