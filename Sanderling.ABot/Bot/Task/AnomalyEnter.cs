using System.Collections.Generic;
using System.Linq;
using Sanderling.Motor;
using Sanderling.Parse;
using BotEngine.Common;
using Sanderling.ABot.Parse;
using WindowsInput.Native;

namespace Sanderling.ABot.Bot.Task
{
	public class AnomalyEnter : IBotTask
	{
		public const string NoSuitableAnomalyFoundDiagnosticMessage = "no suitable anomaly found. waiting for anomaly to appear.";

		public Bot bot;

		static public bool AnomalySuitableGeneral(Interface.MemoryStruct.IListEntry scanResult) =>
			scanResult?.CellValueFromColumnHeader("Name")?.RegexMatchSuccessIgnoreCase("Haven") ?? false;

		public IEnumerable<IBotTask> Component
		{
			get
			{
				var memoryMeasurementAtTime = bot?.MemoryMeasurementAtTime;
				var memoryMeasurementAccu = bot?.MemoryMeasurementAccu;

				var memoryMeasurement = memoryMeasurementAtTime?.Value;

				if (!memoryMeasurement.ManeuverStartPossible())
					yield break;

				var probeScannerWindow = memoryMeasurement?.WindowProbeScanner?.FirstOrDefault();

				var scanResultCombatSite =
					probeScannerWindow?.ScanResultView?.Entry?.FirstOrDefault(AnomalySuitableGeneral);

				if (null == scanResultCombatSite)
					yield return new DiagnosticTask
					{
						MessageText = NoSuitableAnomalyFoundDiagnosticMessage,
					};

				if (null != scanResultCombatSite)
					yield return scanResultCombatSite.ClickMenuEntryByRegexPattern(bot, ParseStatic.MenuEntryWarpToAtLeafRegexPattern);
			 
			}
		}

		public IEnumerable<MotionParam> Effects => null;

		public class ReloadAnomalies : IBotTask
		{
			public const string NoSuitableAnomalyFoundDiagnosticMessage = "no suitable anomaly found. waiting for anomaly to appear.";

			public IEnumerable<IBotTask> Component => null;

			public IEnumerable<MotionParam> Effects
			{
				get
				{
					var APPS = VirtualKeyCode.APPS;

					yield return APPS.KeyboardPress();
					yield return APPS.KeyboardPress();
				}
			}

		}
		public class ReloadAnomalieso : IBotTask
		{
			public const string NoSuitableAnomalyFoundDiagnosticMessage = "no suitable anomaly found. waiting for anomaly to appear.";

			public IEnumerable<IBotTask> Component => null;

			public IEnumerable<MotionParam> Effects
			{
				get
				{
					var APPSo = VirtualKeyCode.VK_1;

					yield return APPSo.KeyboardPress();
					yield return APPSo.KeyboardPress();
					

				}
			}

		}
	}
}
