using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WoWEmissaries.Messages;
using WoWEmissaries.Services;
using Xamarin.Forms;

namespace WoWEmissaries.Droid.Services
{
    [Service]
    public class Wowhead : Service
    {
        CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                try
                {
                    RunParser();
                    Device.StartTimer(new TimeSpan(0, 10, 0), () =>
              {
                    return RunParser();
                });
                }
                catch (Android.OS.OperationCanceledException)
                {
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        Device.BeginInvokeOnMainThread(
                    () => MessagingCenter.Send(new CancelledMessage(), "CancelledMessage")
                  );
                    }
                }

            }, _cts.Token);

            return StartCommandResult.Sticky;
        }

        private bool RunParser()
        {
            if (MockDataStore.factions.Where(f => f.ExpireOn != DateTime.MinValue).Count() < 3)
            {
                using (WowheadParse parser = new WowheadParse())
                {
                    parser.GetEmissaries(_cts.Token).Wait();
                }
            }

            //if (MockDataStore.factions.Where(f => f.ExpireOn.Date == DateTime.Now.Date).Count() == 0 &&
            //  MockDataStore.factions.Where(f => f.ExpireOn.Date != DateTime.MinValue).Count() > 2)
            //  return false;
            //else
            return true;
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}