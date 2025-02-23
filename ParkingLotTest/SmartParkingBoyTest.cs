﻿using ParkingLotManage;
using ParkingLotManage.searchLotStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParkingLotTest
{
    public class SmartParkingBoyTest
    {
        [Fact]
        public void Should_car_be_parked_to_parking_lot_contains_more_positions_when_park_the_car_given_a_smart_parking_boy_manage_two_parking_lots_both_with_available_position_and_a_car()
        {
            //given
            ParkingLot parkingLot1 = new ParkingLot(2);
            ParkingLot parkingLot2 = new ParkingLot(2);
            ParkingBoy smartParkingBoy = new ParkingBoy(new ParkingLot[] {parkingLot1, parkingLot2}, new SmartSearchStrategy());
            Car car1 = new Car();
            Car car2 = new Car();
            smartParkingBoy.Park(car1);
            //when
            Ticket ticket = smartParkingBoy.Park(car2);
            //then
            Assert.Equal(parkingLot2.ParkingLotId, ticket.ParkingLotId);
        }

        [Fact]
        public void Should_return_right_car_with_each_ticket_when_fetch_the_car_twice_given_a_smart_parking_boy_manage_two_parking_lots_both_with_a_parked_car_and_two_parking_ticket()
        {
            //given
            ParkingLot parkingLot1 = new ParkingLot(2);
            ParkingLot parkingLot2 = new ParkingLot(2);
            ParkingBoy smartParkingBoy = new ParkingBoy(new ParkingLot[] { parkingLot1, parkingLot2 }, new SmartSearchStrategy());
            Car car1 = new Car();
            Car car2 = new Car();
            Ticket ticket1 = smartParkingBoy.Park(car1);
            Ticket ticket2 = smartParkingBoy.Park(car2);
            //when
            Car fetchedCar1 = smartParkingBoy.Fetch(ticket1);
            Car fetchedCar2 = smartParkingBoy.Fetch(ticket2);
            //then
            Assert.Equal(car1.ID, fetchedCar1.ID);
            Assert.Equal(car2.ID, fetchedCar2.ID);
        }

        [Fact]
        public void Should_return_nothing_with_error_message_unrecognized_parking_ticket_when_fetch_the_car_given_a_smart_parking_boy_who_manage_two_parking_lots_and_an_unrecognized_ticket()
        {
            // given
            string errMsg = "Unrecognized parking ticket.";
            ParkingLot parkingLot1 = new ParkingLot(2);
            ParkingLot parkingLot2 = new ParkingLot(2);
            ParkingBoy smartParkingBoy = new ParkingBoy(new ParkingLot[] { parkingLot1, parkingLot2 }, new SmartSearchStrategy());
            smartParkingBoy.Park(new Car());
            Ticket unrecognizedTicket = new Ticket();
            //when
            var exception = Assert.Throws<WrongTicketException>(() => smartParkingBoy.Fetch(unrecognizedTicket));
            //then
            Assert.Equal(errMsg, exception.Message);
        }

        [Fact]
        public void Should_return_nothing_with_error_message_unrecognized_parking_ticket_when_fetch_the_car_given_a_smart_parking_boy_who_manage_two_parking_lots_and_a_used_ticket()
        {
            // given
            string errMsg = "Unrecognized parking ticket.";
            ParkingLot parkingLot1 = new ParkingLot(2);
            ParkingLot parkingLot2 = new ParkingLot(2);
            ParkingBoy smartParkingBoy = new ParkingBoy(new ParkingLot[] { parkingLot1, parkingLot2 }, new SmartSearchStrategy());
            Ticket ticket = smartParkingBoy.Park(new Car());
            smartParkingBoy.Fetch(ticket);
            // when
            var exception = Assert.Throws<WrongTicketException>(() => smartParkingBoy.Fetch(ticket));
            //then
            Assert.Equal(errMsg, exception.Message);
        }

        [Fact]
        public void Should_return_nothing_with_error_message_no_available_position_when_park_the_car_given_a_smart_parking_boy_who_manage_two_parking_lots_both_without_any_position_and_a_car()
        {
            //given
            string errMsg = "No available position.";
            ParkingLot parkingLot1 = new ParkingLot(1);
            ParkingLot parkingLot2 = new ParkingLot(2);
            ParkingBoy smartParkingBoy = new ParkingBoy(new ParkingLot[] { parkingLot1, parkingLot2 }, new SmartSearchStrategy());
            smartParkingBoy.Park(new Car());
            smartParkingBoy.Park(new Car());
            smartParkingBoy.Park(new Car());
            //when
            var exception = Assert.Throws<NoPositionException>(() => smartParkingBoy.Park(new Car()));
            //then
            Assert.Equal(errMsg, exception.Message);
        }
    }
}
