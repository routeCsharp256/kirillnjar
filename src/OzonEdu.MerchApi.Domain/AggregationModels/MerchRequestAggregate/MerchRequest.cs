﻿using System;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.Events;
using OzonEdu.MerchApi.Domain.Exceptions;
using OzonEdu.MerchApi.Domain.Exceptions.MerchRequestAggregate;
using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequest : Entity, IAggregationRoot
    {
        public MerchRequest(
            Employee employee
            , int merchPackId
            , MerchRequestDateTime merchRequestDateTime
            , MerchRequestFrom merchRequestFrom)
        {
            Employee = employee ?? throw new RequiredEntityPropertyIsNullException(
                nameof(employee),
                "Merch request must have employee");
            MerchPackId = merchPackId;
            MerchRequestFrom = merchRequestFrom ?? throw new RequiredEntityPropertyIsNullException(
                    nameof(merchRequestFrom),
                    "Merch request must have merch request from type");
            MerchRequestDateTime = merchRequestDateTime ?? throw new RequiredEntityPropertyIsNullException(
                nameof(merchRequestDateTime),
                "Merch request must have date and time");
            MerchRequestStatus = MerchRequestStatus.InWork;
        }
        public MerchRequest(
            Employee employee
            , int merchPackId
            , MerchRequestDateTime merchRequestDateTime
            , MerchRequestStatus merchRequestStatus
            , MerchRequestFrom merchRequestFrom)
            :this(employee, merchPackId, merchRequestDateTime,  merchRequestFrom)
        {
            MerchRequestStatus = merchRequestStatus;
        }

        public MerchRequest(
            int id
            , Employee employee
            , int merchPack
            , MerchRequestDateTime merchRequestDateTime
            , MerchRequestStatus merchRequestStatus
            , MerchRequestFrom merchRequestFrom)
            : this(employee, merchPack, merchRequestDateTime, merchRequestStatus, merchRequestFrom)
        {
            Id = id;
        }

        public MerchRequestStatus MerchRequestStatus { get; private set; }
        public MerchRequestFrom MerchRequestFrom { get; }
        public Employee Employee { get; }
        public int MerchPackId {get;}
        
        public MerchRequestDateTime MerchRequestDateTime { get; private set; }

        public void SetAsAwaitingDelivery(MerchRequestDateTime merchRequestDateTime)
        {
            if (MerchRequestStatus.Equals(MerchRequestStatus.Done))
            {
                throw new MerchRequestStatusException(
                    $"Status {MerchRequestStatus.AwaitingDelivery.Name} can't be set after {MerchRequestStatus.Done.Name}");
            }

            EnsureNotCanceled();
            
            MerchRequestStatus = MerchRequestStatus.AwaitingDelivery;
            MerchRequestDateTime = merchRequestDateTime;
            AddMerchPackReservationFailureDomainEvent();
        }

        public void SetAsDone(MerchRequestDateTime merchRequestDateTime)
        {
            EnsureNotCanceled();
            MerchRequestStatus = MerchRequestStatus.Done;
            MerchRequestDateTime = merchRequestDateTime;
            AddMerchPackReservationSuccessDomainEvent();
        }
        
        public void SetAsCanceled(MerchRequestDateTime merchRequestDateTime)
        {
            if (MerchRequestStatus.Equals(MerchRequestStatus.Done))
            {
                throw new MerchRequestStatusException(
                    $"Status {MerchRequestStatus.Canceled.Name} cannot be set after status {MerchRequestStatus.Done.Name}");
            }

            MerchRequestStatus = MerchRequestStatus.Canceled;
            MerchRequestDateTime = merchRequestDateTime;
        }

        public bool IsIssuedLessYear(DateTime now)
            => now <  MerchRequestDateTime.Value.AddYears(1);

        public bool IsAutomatically() => MerchRequestFrom.Type.Equals(MerchRequestFromType.Automatically);

        private void EnsureNotCanceled()
        {
            if (MerchRequestStatus.Equals(MerchRequestStatus.Canceled))
                throw new MerchRequestStatusException(
                    $"Сannot update a request after it has been canceled");
        }
        
        private void AddMerchPackReservationSuccessDomainEvent()
        {
            INotification merchPackReservationSuccessDomainEvent = new MerchPackReservationSuccessDomainEvent(this);
            AddDomainEvent(merchPackReservationSuccessDomainEvent);
        }
        
        private void AddMerchPackReservationFailureDomainEvent()
        {
            INotification merchPackReservationSuccessDomainEvent = new MerchPackReservationFailureDomainEvent(this);
            AddDomainEvent(merchPackReservationSuccessDomainEvent);
        }
    }
}