#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>

int main()
{
	int sock, listener;
	struct sockaddr_in addr;
	unsigned long freespace;
	int bytes_read;

	listener = socket(AF_INET, SOCK_STREAM, 0);
	if(listener < 0)
	{
		perror("socket");
		exit(1);
	}

	addr.sin_family = AF_INET;
	addr.sin_port = htons(666);
	addr.sin_addr.s_addr = htonl(INADDR_LOOPBACK);

	if(bind(listener, (struct sockaddr *) &addr, sizeof(addr)) < 0)
	{
		perror("bind");
		exit(2);
	}

	listen(listener, 1);

	printf("Server is up!\n");

	while(1)
	{
		sock = accept(listener, 0, 0);
		if(sock < 0)
		{
			perror("accept");
			exit(3);
		}

		printf("Connection received!\n");

		while(1)
		{
			bytes_read = recv(sock, &freespace, sizeof(freespace), 0);
			if(bytes_read != sizeof(freespace))
				break;
			printf("Disk space available: %d bytes\n", freespace);
		}

		close(sock);
	}

	return 0;
}