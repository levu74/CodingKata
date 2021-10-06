def checkPalindrome(inputString):
    return inputString == inputString[::-1]

if __name__ == '__main__':
    print(checkPalindrome('aaabaaaa'))