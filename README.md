# Clones

Clones Version System project. The main method is Execute: the input command and output data are strings.
List of supported commands:
- learn ci pi: teach the clone under number ci programm pi;
After the "learn" command, all history of clones rollbacks is erased.
- rollback ci: rollback last taught program from the clone under number ci;
- relearn ci: reteach the clone under number ci program from last rollback;
- clone ci: make a copy of the clone under number ci;
- check ci: return the last taught program from the clone under number ci (if the clone has no knowledge - return "basic");

Every command returns null (except the check command, which returns the name of a program).

All input commands are correct and cannot lead to unhandled exceptions
(it cannot call a rollback if the clone has only basic knowledge;
relearn is not used when the history of rollbacks is empty; it cannot call for the clone, which does not exist; etc.).

Numbers are assigned to clones in the order of their creation. First default clone has number 1.