using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OBSWebsocketDotNet.Types.Events
{
    /// <summary>
    /// Event args for <see cref="OBSWebsocket.InputVolumeMeters"/>
    /// </summary>
    public class InputVolumeMetersEventArgs : EventArgs
    {
        /// <summary>
        /// List of all Volume Meters included in the update
        /// </summary>
        [JsonProperty(PropertyName = "inputs")]
        public List<VolumeMeter> InputMeters { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="inputs">Collection inputs as JObjects</param>
        public InputVolumeMetersEventArgs(JObject data)
        {
            JsonConvert.PopulateObject(data.ToString(), this);
        }
    }

    /// <summary>
    /// Volume Reading from an Input Source
    /// </summary>
    public class VolumeMeter
    {
        /// <summary>
        /// Name of the Source
        /// </summary>
        [JsonProperty(PropertyName = "inputName")]
        public string InputName { get; set; }

        /// <summary>
        /// Volume recorded by the source.<br/>
        /// The way this is encoded is strange, and not documented at all by the obs-websocket.<br/>
        /// So this just documents what I have found.<br/>
        /// Do not take this behavior as for granted, this is to give you a headstart in understanding what you see.<br/>
        /// <br/>
        /// Dimensionality is the number of audio channels, usually two (even mono inputs return 2, but their values are then identical).<br/>
        /// It is 0 for disconnected audio sources, no volumemeter object is returned for disabled sources,
        /// but normal values are returned for muted sources (just, you know, the values will be 0).<br/>
        /// Left Channel is index 0, Right Channel 1.<br/>
        /// <br/>
        /// The individual channel arrays contain 3 values for some reason.<br/>
        /// Index 1, 2 are the same, is the actual audio output value.<br/>
        /// Values are between 0 and 1, with 0 silence and 1 max,
        /// although when it hits yellow on OBS the number is 0.1, 0.35 when reaching red, 1.0 at peak.<br/>
        /// Index 0 is some strange value that I don't quite understand.<br/>
        /// </summary>
        [JsonProperty(PropertyName = "inputLevelsMul")]
        public double[][] InputLevelsMul { get; set; }

    }
}