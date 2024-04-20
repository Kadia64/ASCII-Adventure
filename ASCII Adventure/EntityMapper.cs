using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Adventure {
    public class EntityMapper {

        private char[,] staticMap;
        public EntityMapper(char[,] startMap) {
            this.staticMap = startMap;
        }
    }
}
