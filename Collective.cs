using System;
using System.Collections.Generic;

namespace mpi {
    public static class Collective {

        public static void ReduceAll<T>(ref T toBeReduced, MPI.ReduceFunc<T> f) {
            MPI.Reduce<T>(0, ref toBeReduced, f);

            //Broadcast here...
            Console.WriteLine("je me souviens");
        }

        public static List<T> Gather<T>(long toWhere, List<T> toBeGathered) {

            return null;
        }

        public static List<T> GatherAll<T>(List<T> toBeGathered) {

            return null;
        }

        public static List<T> Scatter<T>(long fromWhere, List<T> toScatter) {
            Console.WriteLine("je me souviens");
            
            return null;
        }
    }
}
