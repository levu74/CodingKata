def checkPalindrome(inputString):
    if (len(inputString) <= 2):
        return inputString[0] == inputString[-1]
    
    midPoint = len(inputString) // 2
    throtte = 0
    if (len(inputString) % 2 == 0):
        throtte = 1
    for i in range(1, midPoint):
        if (inputString[midPoint - i] != inputString[midPoint + i - throtte]):
            return False
    
    return True

if __name__ == '__main__':
    print(checkPalindrome('aaabaaaa'))