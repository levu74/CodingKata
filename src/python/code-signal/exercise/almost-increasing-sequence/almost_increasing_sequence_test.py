#! python3

import sys
import unittest
from unittest import TestCase
import almost_increasing_sequence
param_list = [([1, 2, 1, 2], False), ([1, 3, 2], True), ([40, 50, 60, 10, 20, 30], False), ([0, -2, 5, 6], True)]
#param_list = [([1, 2, 3, 4, 3, 6], True)]

class Evaluate(TestCase):
    def test_checkPalindrome(self):
        for input, expected in param_list:
            result = almost_increasing_sequence.almostIncreasingSequence(input)
            self.assertEqual(result, expected, 'Input='+ str(input) + '- Expected: ' + str(expected))



if __name__ == '__main__':
    unittest.main()