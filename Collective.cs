using System;
using System.Collections.Generic;

namespace mpi {

    /**
     * Provides functionality for collective MPI functions as part of CSCI-251 HW5
     *
     * @author <a href=mailto:apl1341@rit.edu>Aidan LaBella</a>
     */
    public static class Collective {

        public static void ReduceAll<T>(ref T toBeReduced, MPI.ReduceFunc<T> f) {
            MPI.Reduce<T>(0, ref toBeReduced, f);

            if (MPI.Debugging) {
                Console.WriteLine("ReduceAll");
            }

            //Broadcast here...
        }

        public static List<T> Gather<T>(long toWhere, List<T> toBeGathered) {
            if (MPI.Debugging) {
                Console.WriteLine("Gather");
            }

            return null;
        }

        public static List<T> GatherAll<T>(List<T> toBeGathered) {
            if (MPI.Debugging) {
                Console.WriteLine("GatherAll");
            }

            return null;
        }

        /**
         * Provides functionality for the MPI Scatter function
         *
         * @param fromWhere the node where the scatter is originating from 
         * @param toScatter the collection of elements to scatter
         *
         * @return the scattered collection
         */
        public static List<T> Scatter<T>(long fromWhere, List<T> toScatter) {
            if (MPI.Debugging) {
                Console.WriteLine("Scattering from " + fromWhere);
            }

            List<T> myElements = new List<T>();
            long node = fromWhere;

            long chunkSize = toScatter.Count / MPI.NodeCount;
            Console.WriteLine(chunkSize);

            for (int i = 0; i < toScatter.Count; i++) {
                T element = toScatter[i];
                
                if (fromWhere == node) {
                    myElements.Add(element);
                }

                MPI.SendMsg(node, element);
                Console.WriteLine("Sending: " + element + " to " + node);

                if ((i + 1) % chunkSize == 0) {
                    node = node >= MPI.NodeCount - 1 ? 0 : node + 1;
                }
            }

            for (int i = 0; i < toScatter.Count; i++) {
                T element = toScatter[i];

                if (!toScatter.Contains(element)) {
                    toScatter.RemoveAt(i);
                    i--;
                }
            }

            return myElements;
        }
    }
}
