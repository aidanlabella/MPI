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
            long me = MPI.IAm;
            if (MPI.Debugging) {
                Console.WriteLine("Gathering");
            }

            if (toWhere == me) {
                List<T> gatheredStuff = new List<T>();

                for (long i = 0; i < MPI.NodeCount; i++) {
                    if (i != me) {
                        gatheredStuff.AddRange(MPI.RecvText<List<T>>(i));
                    } else {
                        gatheredStuff.AddRange(toBeGathered);
                    }
                }

                return gatheredStuff;
            } else {
                MPI.SendMsg(toWhere, toBeGathered);

                if (MPI.Debugging) {
                    Console.WriteLine("Sending message as we are not the reciever");
                }

                return toBeGathered;
            }

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

            if (toScatter == null) {
                return MPI.RecvText<List<T>>(fromWhere);
            }

            List<T> myElements = new List<T>();
            List<T> [] nodes = new List<T>[MPI.NodeCount];

            long chunkSize = toScatter.Count / MPI.NodeCount;
            long node = 0;
            for (int j = 0; j < toScatter.Count; j++) {
                T element = toScatter[j];
                
                Console.WriteLine("Sending " + element + " to " + node);

                if (nodes[node] == null) nodes[node] = new List<T>();
                nodes[node].Add(element);

                if (node == fromWhere) {
                    myElements.Add(element);
                }

                if (j % chunkSize == 0 && j != 0) {
                     if (node >= MPI.NodeCount - 1) {
                         if (MPI.Debugging) {
                             Console.WriteLine("Balancing...");
                         }
                     } else {
                         ++node;
                     }
                }
            }

            for (long i = 0; i < MPI.NodeCount; i++) {
                MPI.SendMsg(i, nodes[i]);
            }

            for (int i = 0; i < toScatter.Count; i++) {
                T element = toScatter[i];
                if (!myElements.Contains(element)) {
                    toScatter.RemoveAt(i--);
                }
            }

            return myElements;
        }
    }
}
