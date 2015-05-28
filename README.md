# disk-head-road

## Description
An academic tool for calculating the road length (measured in cyllindes passed by a disk head) for various strategies.

## Sample
```ps
Cyllinders amount? 100
Init cyllinder no? 42
Requests amount? 9
Put requests (space-separated):
51 21 45 75 12 84 36 69 80
strategy (clook,cscan,fcfs,look,scan,sstf,all)? sstf
=========================================================================
0|                                 * (42)
1|                                   * (45)
2|                                        * (51)
3|                             * (36)
4|                  * (21)
5|           * (12)
6|                                                     * (69)
7|                                                         * (75)
8|                                                             * (80)
9|                                                                * (84)
Total distance travelled by head: 120

```
