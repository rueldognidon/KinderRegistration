using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderRegistartion.Services
{
    public class AzureService
    {
        public IMobileServiceClient MobileService { get; set; }
        public IMobileServiceSyncTable<KinderRegistration> SyncTable { get; set; }

        public bool IsBusy { get; set; }

        public async Task Initialize()
        {
            IsBusy = true;
            if (MobileService?.SyncContext?.IsInitialized ?? false)
            {
                IsBusy = false;
                return;
            }

            SQLitePCL.Batteries.Init();

            MobileService = new MobileServiceClient("http://ernidemoapps.azurewebsites.net/");

            var path = "syncstore.db";
            var store = new MobileServiceSQLiteStore(path);

            store.DefineTable<KinderRegistration>();

            try
            {
                await MobileService.SyncContext.InitializeAsync(store);

                SyncTable = MobileService.GetSyncTable<KinderRegistration>();
            }
            catch {
                IsBusy = false;
            }


            IsBusy = false;
        }

        public async Task<IEnumerable<KinderRegistration>> GetList(bool synced = false)
        {
            await Initialize();

            if (synced)
                await Sync();

            return await SyncTable.ToEnumerableAsync();
        }
        public async Task<KinderRegistration> AddItem(KinderRegistration item)
        {
            await Initialize();

            await SyncTable.InsertAsync(item);
            await Sync();
            return item;
        }

        public async Task<KinderRegistration> UpdateItem(KinderRegistration item)
        {
            await Initialize();

            await SyncTable.UpdateAsync(item);
            await Sync();
            return item;
        }

        public async Task DeleteItem(string id)
        {
            await Initialize();

            var item = await SyncTable.LookupAsync(id);
            await SyncTable.DeleteAsync(item);
        }

        public async Task Sync()
        {
            try
            {
                //if (!_connectivity.IsConnected) return;

                await SyncTable.PullAsync($"KinderRegistration", SyncTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync coffees, that is alright as we have offline capabilities: " + ex);
            }
        }
    }
}
