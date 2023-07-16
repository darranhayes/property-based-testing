# Property Based Testing

This repository reproduces a failing test described [here](https://ardalis.com/never-use-the-same-value-for-two-ids-or-other-values-in-your-tests/#sq_hafou10r4v) by Steve "ardalis" Smith, and explores whether using a property based testing approach solves this problem by default.

Two property-based-testing libraries have been used here with the XUnit test framework:
- [x] [CsCheck](https://github.com/AnthonyLloyd/CsCheck)
- [ ] [FsCheck](https://github.com/fscheck/FsCheck)