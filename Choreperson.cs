//Java code literally copy and pasted from Chorebot with only the toSring method needing a C# tweak...

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chorebotnet {
    [JsonObject]
    public class Choreperson {
        [JsonProperty]
        private String name;
        [JsonProperty]
        private String username;
        [JsonProperty]
        private Boolean paid;
        [JsonProperty]
        private int owes;
        [JsonProperty]
        private long id;

        public Choreperson() {
            this.name = "TEMP";
            this.setUsername("TEMP");
            this.paid = false;
            this.owes = 0;
            this.setId(0L);
        }

        public Choreperson(String name, String username, long id) {
            this.name = name;
            this.setUsername(username);
            this.paid = false;
            this.owes = 0;
            this.setId(id);
        }

        public String getName() {
            return name;
        }
        public void setName(String name) {
            this.name = name;
        }
        public Boolean hasPaid() {
            return paid;
        }
        public void paidRent(Boolean paid) {
            this.paid = paid;
        }
        public int getOwes() {
            return owes;
        }
        public void setOwes(int owes) {
            this.owes = owes;
        }

        public long getId() {
            return id;
        }

        public void setId(long id) {
            this.id = id;
        }

        public String getUsername() {
            return username;
        }

        public void setUsername(String username) {
            this.username = username;
        }

        public override String ToString() {
            return this.name;
        }
    }
}
