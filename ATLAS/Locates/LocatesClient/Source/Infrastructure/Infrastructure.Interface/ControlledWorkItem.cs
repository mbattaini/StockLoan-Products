//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The ControlledWorkItem represents a WorkItem that is managed by another class. 
// The other class is the controller and contains the business logic 
// (the WorkItem provides the container.)
// 
// For more information see: 
// ms-help://MS.VSCC.v90/MS.VSIPCC.v90/ms.practices.scsf.2008apr/SCSF/html/03-01-010-How_to_Create_Smart_Client_Solutions.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using Microsoft.Practices.CompositeUI;

namespace StockLoan.Locates.Infrastructure.Interface
{
    /// <summary>
    /// Represents a WorkItem that uses a WorkItem controller to perform its business logic.
    /// </summary>
    /// <typeparam name="TController"></typeparam>
    public sealed class ControlledWorkItem<TController> : WorkItem
    {
        private TController _controller;

        /// <summary>
        /// Gets the controller.
        /// </summary>
        public TController Controller
        {
            get { return _controller; }
        }

        /// <summary>
        /// See <see cref="M:Microsoft.Practices.ObjectBuilder.IBuilderAware.OnBuiltUp(System.String)"/> for more information.
        /// </summary>
        public override void OnBuiltUp(string id)
        {
            base.OnBuiltUp(id);

            _controller = Items.AddNew<TController>();
        }
    }
}
