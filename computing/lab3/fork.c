#include <stdio.h>
#include <stdlib.h>
#include <errno.h>

void main(int argc, const char* argv[], const char* envp[]) 
{
	const char* parent_fname = argv[1];
	const char* child_fname = argv[2];

	pid_t cpid = fork();

	FILE* fd = fopen(cpid ? parent_fname : child_fname, "w");
	
	fprintf(fd, "%s here\n", cpid ? "Parent" : "Child");
	fprintf(fd, "My pid: %d\n", getpid());
	fprintf(fd, "Parent pid: %d\n", cpid ? getpid() : getppid());
	fprintf(fd, "Child pid: %d\n", cpid ? cpid : getpid());
}
