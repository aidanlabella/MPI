using System;

namespace mpi {
    public static class Collective {

        public static void ReduceAll<T>(ref T toBeReduced, MPI.ReduceFunc<T> f) {
            Console.WriteLine("Je me souviens");
        }

        public static List<T> Gather<T>(long toWhere, List<T> toBeGathered) {

        }

        public static List<T> GatherAll<T>(List<T> toBeGathered) {

        }

        public static List<T> Scatter<T>(long fromWhere, List<T> toScatter) {

        }

        //Used for testing
        // public static void Main(string [] args) {
        //     Console.WriteLine("What");
        //     Console.Beep();
        // }

    }
}
