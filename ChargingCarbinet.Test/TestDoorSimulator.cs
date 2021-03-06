﻿using System;
using System.Collections.Generic;
using System.Text;
using ChargingCabinet;
using ChargingCabinet.Events;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestDoorSimulator
    {
        private DoorSimulator _uut;
        private DoorOpenEventArgs _doorOpenEventArgs;
        private DoorCloseEventArgs _doorCloseEventArgs;

        [SetUp]
        public void Setup()
        {
            _doorOpenEventArgs = null;
            _doorCloseEventArgs = null;
            _uut = new DoorSimulator();

            _uut.DoorOpenEvent +=
                (o, args) =>
                {
                    _doorOpenEventArgs = args;
                };


            _uut.DoorCloseEvent +=
                (o, args) =>
                {
                    _doorCloseEventArgs = args;
                };
        }

        [Test]
        public void IsLocked_IsTrue_ByDefault()
        {
            Assert.That(_uut.IsLocked, Is.True);
        }

        [Test]
        public void LockDoor_IsLocked_IsTrue()
        {
            _uut.LockDoor();
            Assert.That(_uut.IsLocked, Is.True);
        }

        [Test]
        public void UnlockDoor_IsLocked_IsFalse()
        {
            _uut.UnlockDoor();
            Assert.That(_uut.IsLocked, Is.False);
        }

        [Test]
        public void ctor_DoorOpenedValue_IsFalse()
        {
            Assert.That(_uut.DoorOpenedValue, Is.False);
        }

        [Test]
        public void ctor_DoorClosedValue_IsTrue()
        {
            Assert.That(_uut.DoorClosedValue, Is.True);
        }

        [Test]
        public void OnDoorOpen_DoorOpenEventArgs_IsNull()
        {
            Assert.That(_doorOpenEventArgs, Is.Null);
        }

        [Test]
        public void DoorCloseEventArgs_IsNull()
        {
            Assert.That(_doorCloseEventArgs, Is.Null); 
        }

        [Test]
        public void OnDoorOpen_DoorOpenEventArgs_IsNotNull()
        {
            _uut.OnDoorOpen(true);

            Assert.Multiple(() =>
            {
                Assert.That(_doorOpenEventArgs, Is.Not.Null);
                Assert.That(_doorOpenEventArgs.DoorOpened, Is.EqualTo(true)); 
                Assert.That(_doorCloseEventArgs, Is.Null);
            });
        }

        [Test]
        public void OnDoorClose_DoorCloseEventArgs_IsNotNull()
        {
            _uut.OnDoorClose(true);

            Assert.Multiple(() =>
            {
                Assert.That(_doorCloseEventArgs, Is.Not.Null);
                Assert.That(_doorCloseEventArgs.DoorClosed, Is.EqualTo(true));
                Assert.That(_doorOpenEventArgs, Is.Null);
            });

        }

        [Test]
        public void OnDoorOpen_DoorOpenedValue_IsEqualTo_DoorOpened()
        {
            var newBool = _uut.DoorOpenedValue;
            _uut.OnDoorOpen(newBool);
            Assert.That(_doorOpenEventArgs.DoorOpened, Is.EqualTo(newBool));
        }

        [Test]
        public void OnDoorClosed_DoorClosedValue_IsEqualTo_DoorClosed()
        {
            var newBool = _uut.DoorClosedValue;
            _uut.OnDoorClose(newBool);
            Assert.That(_doorCloseEventArgs.DoorClosed, Is.EqualTo(newBool));

        }

        [TestCase(false)]
        public void DoorOpenedValue_IsEqualTo_Argument(bool newBool)
        {
            _uut.DoorOpenEvent += Raise.EventWith(new DoorOpenEventArgs() { DoorOpened = newBool });
            Assert.That(_uut.DoorOpenedValue, Is.EqualTo(newBool));
        }

        [TestCase(true)]
        public void DoorClosedValue_IsEqualTo_Argument(bool newBool)
        {
            _uut.DoorCloseEvent += Raise.EventWith(new DoorCloseEventArgs() { DoorClosed = newBool });
            Assert.That(_uut.DoorClosedValue, Is.EqualTo(newBool));
        }
    }
}
