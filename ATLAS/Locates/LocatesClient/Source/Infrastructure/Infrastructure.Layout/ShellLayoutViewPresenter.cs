using Microsoft.Practices.CompositeUI.EventBroker;
using StockLoan.Locates.Infrastructure.Interface;
using StockLoan.Locates.Infrastructure.Interface.Constants;

namespace StockLoan.Locates.Infrastructure.Layout
{
    public class ShellLayoutViewPresenter : Presenter<ShellLayoutView>
    {
        protected override void OnViewSet()
        {
            WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainMenu, View.MainMenuStrip);
            WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainStatus, View.MainStatusStrip);
            WorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainToolbar, View.MainToolbarStrip);
        }

        /// <summary>
        /// Status update handler. Updates the status strip on the main form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopicNames.StatusUpdate, ThreadOption.UserInterface)]
        public void StatusUpdateHandler(object sender, EventArgs<string> e)
        {
            View.SetStatusLabel(e.Data);
        }

        /// <summary>
        /// Called when the user asks to exit the application.
        /// </summary>
        public void OnFileExit()
        {
            View.ParentForm.Close();
        }
    }
}
