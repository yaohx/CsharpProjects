using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace MB.WcfServiceLocator.ClientChannel {
    internal class EndpointMessageBehavior : IEndpointBehavior {
        /// <summary>
        /// AddBindingParameters
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) {

        }
        /// <summary>
        /// ApplyClientBehavior
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime) {
            if (WcfClientChannelFactory.ClientMessageInspectors != null) {
                foreach (var inspector in WcfClientChannelFactory.ClientMessageInspectors)
                    clientRuntime.MessageInspectors.Add(inspector);
            }

            if (WcfServiceInvokeScope.Current != null) {
                foreach (var inspector in WcfServiceInvokeScope.Current.ClientMessageInspector)
                    clientRuntime.MessageInspectors.Add(inspector);
            }
        }
        /// <summary>
        /// ApplyDispatchBehavior
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher) {
            // throw new NotImplementedException();
        }
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="endpoint"></param>
        public void Validate(ServiceEndpoint endpoint) {
            // throw new NotImplementedException();
        }
    }

}
