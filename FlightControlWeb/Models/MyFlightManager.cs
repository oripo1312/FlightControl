﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class MyFlightManager : IFlightManager
    {
        public Flight CreateUpdatedFlight(FlightPlan flightPlan, DateTime relativeTime)
        {
            double secondsTimeSpan = SecondsGap(flightPlan.InitialLocation.DateTime, relativeTime);
            if (secondsTimeSpan < 0)
            {
                return null;
            }
            List<Segment> segments = flightPlan.Segments;
            double totalFlightTime = 0;
            double secondsAtCurrentSegment = 0;
            Segment prevSegment = InitialLocationToSegment(flightPlan);
            int i = 0;
            for (i = 0; i < segments.Count; i++)
            {
                totalFlightTime += segments[i].TimespanSeconds;
                if (secondsTimeSpan < totalFlightTime)
                {
                    secondsAtCurrentSegment = secondsTimeSpan - totalFlightTime + segments[i].TimespanSeconds;
                    break;
                }
                prevSegment = segments[i];
            }
            if (i == segments.Count)
            {
                return null;
            }
            Point p1 = new Point { X = prevSegment.Longitude, Y = prevSegment.Latitude };
            Point p2 = new Point { X = segments[i].Longitude, Y = segments[i].Latitude };
            Line line = new Line { StartPoint = p1, EndPoint = p2 };
            Point currentPoint = line.GetPointOnLine(secondsAtCurrentSegment / segments[i].TimespanSeconds);
            Flight updatedFlight = CreateCurrentFlight(flightPlan, currentPoint);
            return updatedFlight;
        }

        //this function calculates the distance in seconds between the relative time to flight departure
        public double SecondsGap(DateTime flightPlanInitialTime, DateTime relativeTime)
        {
            TimeSpan timeSpan = relativeTime - flightPlanInitialTime;
            double secondsTimeSpan = timeSpan.TotalSeconds;
            return secondsTimeSpan;

        }

        public Segment InitialLocationToSegment(FlightPlan flightPlan)
        {
            Segment segment = new Segment();
            segment.Latitude = flightPlan.InitialLocation.Latitude;
            segment.Longitude = flightPlan.InitialLocation.Longitude;
            segment.TimespanSeconds = 0;
            return segment;
        }
        public Flight CreateCurrentFlight(FlightPlan flightPlan, Point currentLocation)
        {
            Flight flight = new Flight();
            flight.CompanyName = flightPlan.CompanyName;
            flight.FlightId = flightPlan.FlightPlanId;
            flight.IsExternal = false;
            flight.Latitude = currentLocation.X;
            flight.Longitude = currentLocation.Y;
            flight.Passengers = flightPlan.Passengers;
            //flight.DateTime ??
            return flight;
        }
    }
}
