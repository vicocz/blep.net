using Blep.Contract;
using Blep.Contract.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace Blep.Framework.Discovery
{

    public partial class SimpleDiscovery
    {
        private class DiscoveryTask : IDisposable
        {
            // required property strings: see https://msdn.microsoft.com/en-us/library/windows/desktop/ff521659(v=vs.85).aspx
            static readonly string[] baseProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected", "System.Devices.Aep.Bluetooth.Le.IsConnectable" };

            // AQS filter
            private const string bleDeviceFilter = "(System.Devices.Aep.ProtocolId:=\"{bb7bb05e-5972-42b5-94fc-76eaa7084d49}\")";

            private readonly object lockItem = new object();

            private DeviceWatcher deviceWatcher;
            private readonly TaskCompletionSource<bool> taskCompletionSource;

            private Dictionary<string, DeviceInformation> devices = new Dictionary<string, DeviceInformation>();

            public DiscoveryTask()
            {
                taskCompletionSource = new TaskCompletionSource<bool>();
            }

            public Task<IEnumerable<DiscoveredDevice>> Start(TimeSpan maxExecutionTime)
            {
                StartDeviceWatcher();

                return Task.Factory.StartNew<IEnumerable<DiscoveredDevice>>(() =>
                   {
                       taskCompletionSource.Task.Wait(maxExecutionTime);
                       return GetResults();
                   });
            }

            private IEnumerable<DiscoveredDevice> GetResults()
            {
                lock (lockItem)
                {
                    return devices.Values.
                        Select(d => d.ToDiscoveredDevice())
                        .ToList();
                }
            }

            public IEnumerable<DiscoveredDevice> EnumerateDevices(TimeSpan timeout)
            {
                try
                {

                    StartDeviceWatcher();

                    var result = taskCompletionSource.Task.Wait(timeout);
                }

                finally
                {
                    StopDeviceWatcher();
                }

                return GetResults();
            }

            private void StartDeviceWatcher()
            {
                deviceWatcher =
                         DeviceInformation.CreateWatcher(
                             bleDeviceFilter,
                             baseProperties,
                             DeviceInformationKind.AssociationEndpoint);

                // Register event handlers before starting the watcher.
                deviceWatcher.Added += DeviceWatcher_Added;
                deviceWatcher.Updated += DeviceWatcher_Updated;
                deviceWatcher.Removed += DeviceWatcher_Removed;
                deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
                deviceWatcher.Stopped += DeviceWatcher_Stopped;

                // Start the watcher.
                deviceWatcher.Start();
            }

            private void StopDeviceWatcher()
            {
                if (deviceWatcher != null)
                {
                    // Unregister the event handlers.
                    deviceWatcher.Added -= DeviceWatcher_Added;
                    deviceWatcher.Updated -= DeviceWatcher_Updated;
                    deviceWatcher.Removed -= DeviceWatcher_Removed;
                    deviceWatcher.EnumerationCompleted -= DeviceWatcher_EnumerationCompleted;
                    deviceWatcher.Stopped -= DeviceWatcher_Stopped;

                    // Stop the watcher.
                    deviceWatcher.Stop();
                    deviceWatcher = null;
                }
            }

            private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation deviceInfo)
            {
                lock (lockItem)
                {
                    Debug.WriteLine($"Added {deviceInfo.Id}{deviceInfo.Name}");

                    // Protect against race condition if the task runs after the app stopped the deviceWatcher.
                    if (sender == deviceWatcher)
                    {
                        devices[deviceInfo.Id] = deviceInfo;

                    }
                }
            }

            private void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
            {
                lock (lockItem)
                {
                    Debug.WriteLine($"Updated {deviceInfoUpdate.Id}");

                    // Protect against race condition if the task runs after the app stopped the deviceWatcher.
                    if (sender == deviceWatcher)
                    {
                        var info = devices[deviceInfoUpdate.Id];

                        info.Update(deviceInfoUpdate);
                    }
                }
            }

            private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate deviceInfoUpdate)
            {
                lock (lockItem)
                {
                    Debug.WriteLine($"Removed {deviceInfoUpdate.Id}");

                    // Protect against race condition if the task runs after the app stopped the deviceWatcher.
                    if (sender == deviceWatcher)
                    {
                        this.devices.Remove(deviceInfoUpdate.Id);
                    }
                }
            }

            private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object e)
            {
                taskCompletionSource?.SetResult(true);
            }

            private void DeviceWatcher_Stopped(DeviceWatcher sender, object e)
            {
                taskCompletionSource?.SetResult(false);
                //                            sender.Status == DeviceWatcherStatus.Aborted ? NotifyType.ErrorMessage : NotifyType.StatusMessage);
            }

            public void Dispose()
            {
                StopDeviceWatcher();
            }
        }

    }
}
