#include <fcntl.h>
#include <stdlib.h>
#include <errno.h>
#include <signal.h>

int pipefd[2];
int odd_cpid, even_cpid;
int other_pid;
volatile int gate = 1;

void sigusr1_handler(int sigval) 
{
	gate = 0;
}

int fork_me()
{
	int cpid = fork();
	if(cpid == 0)
		signal(SIGUSR1, sigusr1_handler);
	return cpid;
}

void main(int argc, const char* argv[], const char* envp[]) 
{
	int eof_reached = 0;
	char c;

	pipe(pipefd);

	int odd_cpid = fork_me();
	if(odd_cpid == 0)
	{
		odd_cpid = getpid();
		even_cpid = fork_me();
		
		close(pipefd[1]);

		if(even_cpid == 0)
			other_pid = odd_cpid, gate = 0;
		else
			other_pid = even_cpid, gate = 1;
		
		while(!eof_reached)
		{
			while(gate == 1);
			if(read(pipefd[0], &c, 1))
				write(1, &c, 1),
				printf("%s says: %.1s", getpid() == even_cpid ? "Even" : "Odd", c),
				gate = 1;
			else
				eof_reached = 1;
			kill(other_pid, SIGUSR1);	
		}
	} 
	else 
	{
		int fd;
		char c;
		
		close(pipefd[0]);
		
		fd = open(argv[1], O_RDONLY);

		while(read(fd, &c, 1))
			write(pipefd[1], &c, 1);

		close(pipefd[1]);

		wait(0);
	}
}
