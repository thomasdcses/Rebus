﻿using System;
using Rebus.Config;
using Rebus.Pipeline;
using Rebus.Pipeline.Send;
using Rebus.Transport;

namespace Rebus.Auditing
{
    /// <summary>
    /// Configuration extensions for the auditing configuration
    /// </summary>
    public static class AuditingConfigurationExtensions
    {
        /// <summary>
        /// Enables message auditing whereby Rebus will forward to the audit queue a copy of each properly handled message and
        /// each published message
        /// </summary>
        public static void EnableMessageAuditing(this OptionsConfigurer configurer, string auditQueue)
        {
            if (configurer == null) throw new ArgumentNullException("configurer");
            if (string.IsNullOrWhiteSpace(auditQueue)) throw new ArgumentNullException("auditQueue");

            configurer.Register(c => new OutgoingAuditingStep(auditQueue, c.Get<ITransport>()));

            configurer.Decorate<IPipeline>(c => new PipelineStepInjector(c.Get<IPipeline>())
                .OnSend(c.Get<OutgoingAuditingStep>(), PipelineRelativePosition.After, typeof(SendOutgoingMessageStep)));

            configurer.Register(c => new IncomingAuditingStep(auditQueue, c.Get<ITransport>()));

            configurer.Decorate<IPipeline>(c => new PipelineStepConcatenator(c.Get<IPipeline>())
                .OnReceive(c.Get<IncomingAuditingStep>(), PipelineAbsolutePosition.Front));
        }
    }
}