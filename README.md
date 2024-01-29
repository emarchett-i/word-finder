# word-finder

This is a Console Application that uses .NET 8.

Problem: 
This application is to solve the Word Finder challenge (please see the Qu Developer Challenge-Word Finder.pdf file in this project)

Solution:
The solution consists on having a class 'WordFinder" with the responsibility of finding the given words on a given square matrix (64x64 or smaller)

The WordFinder class uses recursion to iterate over the cells to find the words in both directions (Horizontal and Vertical). Considering every cell as a starting point, we analyze if the words that are being formed with it as a starting point in both directions matches the words that we try to find; for performance improvement we only consider the cells that starts with any of the words we are looking for.

Tests cases provided via UnitTests
- Test for the standard case provided by the challenge via PDF.
- If there are no matches then it should return an empty list.
- If a word is found more than once it should appear only once in the result.
- The result should contain the top 10 matches.
- The matrix should be a square matrix (n x n).
- The matrix size should not be more than 64.
