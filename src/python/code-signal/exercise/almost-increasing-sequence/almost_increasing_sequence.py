#! python3
import copy
def almostIncreasingSequence(sequence):
    copySequence = copy.copy(sequence)
    isRemoved = False
    increaseNumbers = [];
    while len(copySequence) > 1: 
        if copySequence[0] < copySequence[1] \
            and (len(increaseNumbers) == 0 or copySequence[0] > increaseNumbers[-1]):
            increaseNumbers += [copySequence[0]]
            del copySequence[0]
        elif isRemoved:
            break
        else:
            isRemoved = True
            if len(copySequence) > 2:
                if (copySequence[1] < copySequence[2] and copySequence[0] >= copySequence[2]):
                    del copySequence[0]
                else:
                    del copySequence[1]
            else:
                del copySequence[0]
    return len(copySequence) == 1
