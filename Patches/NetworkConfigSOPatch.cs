﻿using HarmonyLib;

namespace BeatTogether.Patches
{
    [HarmonyPatch(typeof(NetworkConfigSO), "masterServerStatusUrl", MethodType.Getter)]
    internal class GetMasterServerStatusUrlPatch
    {
        internal static void Postfix(ref string __result)
        {
            var server = Plugin.ServerProvider.Selection;
            if (server.IsOfficial)
            {
                Plugin.Logger.Debug("Playing on official servers.");
                return;
            }

            // TODO: add server specific url here
            __result = Plugin.Configuration.StatusUrl;
            Plugin.Logger.Info($"Patching master server status URL (URL='{__result}').");
        }
    }

    [HarmonyPatch(typeof(NetworkConfigSO), "masterServerEndPoint", MethodType.Getter)]
    internal class GetMasterServerEndPointPatch
    {
        internal static void Postfix(ref MasterServerEndPoint __result)
        {
            var server = Plugin.ServerProvider.Selection;
            if (server.IsOfficial)
            {
                Plugin.Logger.Debug("Playing on official servers.");
                return;
            }

            __result = server.EndPoint;
            Plugin.Logger.Debug($"Patching master server end point (EndPoint='{__result}').");
        }
    }
}
