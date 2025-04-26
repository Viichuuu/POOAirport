using System;

namespace PracticalWorkI
{
    public class Runway
    {
        private string id;
        private RunwayStatus status;
        private Aircraft currentAircraft;
        private int ticksAvailability;

        public Runway(string id)
        {
            this.id = id;
            this.status = RunwayStatus.Free;
            this.currentAircraft = null;
            this.ticksAvailability = 0;
        }

        public string GetID() => id;
        public RunwayStatus GetStatus() => status;
        public Aircraft GetCurrentAircraft() => currentAircraft;
        public int GetTicksAvailability() => ticksAvailability;

        public void ReserveRunmway(Aircraft aircraft)
        {
            this.currentAircraft = aircraft;
            this.status = RunwayStatus.Ocupated;
            this.ticksAvailability = 3;
        }

        public void FreeRunway()
        {
            this.currentAircraft = null;
            this.status = RunwayStatus.Free;
            this.ticksAvailability = 0;
        }

        public void DecreaseTicksAvailability()
        {
            if (ticksAvailability > 0)
            {
                ticksAvailability--;
            }
            if (ticksAvailability == 0 && currentAircraft != null)
            {
                FreeRunway();
            }
        }
        public override string ToString()
        {
            string aircraftInfo = (currentAircraft != null) ? currentAircraft.GetID() : "None";
            return $"Runway {id} - Status: {status}, Aircraft: {aircraftInfo}, Ticks to Free: {ticksAvailability}";
        }
    }
}