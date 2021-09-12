A format for expressing an ordered list of integers is to use a comma separated list of either

*   individual integers
*   or a range of integers denoted by the starting integer separated from the end integer in the range by a dash, '-'. The range includes all integers in the interval including both endpoints. It is not considered a range unless it spans at least 3 numbers. For example "12,13,15-17"

Complete the solution so that it takes a list of integers in increasing order and returns a correctly formatted string in the range format.

**Example:**

    solution([-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20]);
    // returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    solution([-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20])
    # returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    solution([-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20])
    # returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    solution([-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20])
    # returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    Solution.rangeExtraction(new int[] {-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20})
    # returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    RangeExtraction.Extract(new[] {-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20});
    # returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    range_extraction({-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20});
    // returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    range_extraction((const []){-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20}, 20);
    /* returns "-6,-3-1,3-5,7-11,14,15,17-20" */
    

    nums:  dd  -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20
    
    mov rdi, nums
    mov rsi, 20
    call range_extraction
    ; RAX <- `-6,-3-1,3-5,7-11,14,15,17-20\0`
    

    rangeextraction([-6 -3 -2 -1 0 1 3 4 5 7 8 9 10 11 14 15 17 18 19 20])
    # returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    solution(List(-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20))
    // "-6,-3-1,3-5,7-11,14,15,17-20"
    

    (solution '(-6 -3 -2 -1 0 1 3 4 5 7 8 9 10 11 14 15 17 18 19 20))
    ; returns "-6,-3-1,3-5,7-11,14,15,17-20"
    

    solution([-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20])
    // returns '-6,-3-1,3-5,7-11,14,15,17-20'
    

_Courtesy of rosettacode.org_

* * *

Algorithms

String Formatting

Formatting

Strings