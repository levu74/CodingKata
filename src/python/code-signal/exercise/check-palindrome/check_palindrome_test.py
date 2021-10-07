import sys
import unittest
from unittest import TestCase
import check_palindrome
param_list = [('aaabaaaa', False), ('abacaba', True), ('abac', False), ('hlbeeykoqqqqokyeeblh', True)]

class Evaluate(TestCase):
    def test_checkPalindrome(self):
        for input, expected in param_list:
            result = check_palindrome.checkPalindrome(input)
            self.assertEqual(result, expected, 'Input='+ input + '- Expected: ' + str(expected))



if __name__ == '__main__':
    unittest.main()