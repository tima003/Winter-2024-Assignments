# Winter 2024 Assignment 02 - Control Structures and Error Handling
__Weight:__ 10% of final mark

__Submission requirements:__ On or before the deadline, commit a Visual Studio 2022 project to the GitHub repository. __You must commit and push to the classroom repository supplied for the assignment__; do not create your own repository. It is your responsibility to ensure that your work is in the correct repository. ___Work not in the repository will not be graded___.

## Crypto Trading on DMITCryptEx
_NOTE: you do not have to know anything about cryptocurrency or Ethereum to complete this assignment._

[Cryptocurrency](https://en.wikipedia.org/wiki/Cryptocurrency) (crypto) trading has become more mainstream over the past few years. Ether (ETH) is one of the better-known cryptocurrencies, and provides a lower-risk crypto exposure vehicle for new crypto investors. ETH is backed by lively community and the technology used to support it can be harnessed to develop decentralized applications using the Ethereum network. However, most folks are simply interested in holding the ETH cryptocurrency as an investment that will appreciate or by earning rewards from
[staking](https://ethereum.org/en/staking/) their Ether.

### Requirements
The program will need to prompt the user for several inputs, namely:

- The fractional amount of ETH the user would like to purchase (must be greater than zero)
- Whether the user would like to stake their ETH or not

The program will apply a commision (charged by the centralized exchange) to the trade using the following table:

| ETH Purchased | Commission Rate |
|---|---|
| 0.000001 - 0.999999 | 1.9% |
| 1.000000 - 4.999999 | 1.75% |
| 5.000000 - 9.999999 | 1.5 |
| 10.0000 or more | 1.25% |

As you can see, the more ETH a user purchases, the lower the charged commission. If the user chooses to stake their ETH, calculate and display the monthly reward amount. The current annual reward rate offerred by the exchange is 3.1% using [simple interest](https://brilliant.org/wiki/simple-interest/#simple-interest-explained).

The program must display all the calculated results to the user and ask them to confirm their trade. If the user confirms the trade, display a message thanking them for using DMITCryptEx. If the user cancels the trade, display a message confirming that the trade has been cancelled.

_NOTE: You will need to develop your own test plan for this assignment._

### Sample Runs
#### Purchase and confirm
// TODO

#### Purchase and cancel
// TODO

#### Purchase, stake, and confirm
// TODO

## Submission
Commit and push your solution to your GitHub classroom assignment repository before the deadline. Ensure that your solution follows the best coding and style practices, as your instructor has shown you in class. Failed adherence to the prescribed style guidelines may result in lost marks. __Your program must compile; a program that fails to compile will not be graded.__

_NOTE: push early and often to your repository to recieve feedback from your instructor prior to the deadline. Your instructor will not be providing feedback for every commit every day. However, the eariler and more often you commit, the greater the chances of your instructor reviewing your work and providing constructive feedback that you can act on before the deadline._

## Rubric
| Mark | Description |
|---|---|
| 5  | Excellent – program passes all test cases and coding follows best practices and class standards |
| 4  | Very Good – program passes all test cases, but coding does not follow best practices and class standards |
| 3  | Acceptable – coded most the requirements and program produces the expected results for some of the test cases |
| 2  | Needs Work – coded some the requirements but program fails to produce expected results |
| 1  | Unsatisfactory – code does not meet any of the requirements |
| 0  | Not done |